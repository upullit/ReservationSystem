using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReservationSystem.Areas.User.Models;

namespace ReservationSystem.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Login GET method
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/"); // Default redirect to home page

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
            // Debugging - Check if model values are coming through correctly
            Console.WriteLine($"Username: {model.Username}");
            Console.WriteLine($"Password: {model.Password}");
            Console.WriteLine($"RememberMe: {model.RememberMe}");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Username);

                if (user != null)
                {
                    // Validate password
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // Set success message in TempData
                        TempData["SuccessMessage"] = "Login successful!";

                        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                        var isStaff = await _userManager.IsInRoleAsync(user, "Staff");

                        if (isAdmin || isStaff)
                        {
                            return RedirectToAction("Dashboard", "Admin", new { area = "Admin" }); // Redirect to Admin Dashboard
                        }

                        // If not an admin, redirect to the original requested URL or home
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            // Get roles from RoleManager
            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,  // Role name is the value
                Text = r.Name    // Role name is the displayed text
            }).ToList();

            // Initialize the RegisterViewModel and set the Roles property
            var model = new RegisterViewModel
            {
                Roles = roles  // Make sure Roles is initialized
            };

            return View(model);
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Ensure the role exists before assigning
                    if (!string.IsNullOrEmpty(model.SelectedRole))
                    {
                        var roleExists = await _roleManager.RoleExistsAsync(model.SelectedRole);
                        if (roleExists)
                        {
                            await _userManager.AddToRoleAsync(user, model.SelectedRole);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Role does not exist.");
                        }
                    }

                    // Sign in the user after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
                }
                else
                {
                    // Collect and display error messages
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        // Logout method
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }

}
