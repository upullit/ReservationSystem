using Microsoft.AspNetCore.Identity;

namespace ReservationSystem.Data.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Member", "Staff" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task AssignRoles(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Try to find the admin user by email
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");

            // If the admin user does not exist, create a new one
            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "admin", Email = "admin@example.com" };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (!result.Succeeded)
                {
                    // If user creation fails, log the errors
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating admin user: {error.Description}");
                    }
                    return; // Exit if user creation fails
                }
            }

            // Ensure the admin user is in the "Admin" role
            if (!(await userManager.IsInRoleAsync(adminUser, "Admin")))
            {
                var roleExists = await roleManager.RoleExistsAsync("Admin");
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin")); // Create the role if it doesn't exist
                }
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Create and assign a "Staff" role and user
            var staffUser = await userManager.FindByEmailAsync("staff@example.com");

            // If the staff user does not exist, create a new one
            if (staffUser == null)
            {
                staffUser = new IdentityUser { UserName = "staff", Email = "staff@example.com" };
                var result = await userManager.CreateAsync(staffUser, "Staff@123");

                if (!result.Succeeded)
                {
                    // If user creation fails, log the errors
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating staff user: {error.Description}");
                    }
                    return; // Exit if user creation fails
                }
            }

            // Ensure the staff user is in the "Staff" role
            if (!(await userManager.IsInRoleAsync(staffUser, "Staff")))
            {
                var roleExists = await roleManager.RoleExistsAsync("Staff");
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole("Staff")); // Create the role if it doesn't exist
                }
                await userManager.AddToRoleAsync(staffUser, "Staff");
            }
        }
    }
}

