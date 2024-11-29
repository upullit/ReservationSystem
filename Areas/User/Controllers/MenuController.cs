using Microsoft.AspNetCore.Mvc;
using MyReservationSystem.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MyReservationSystem.Areas.User.Controllers
{
    [Area("User")]
    public class MenuController : Controller
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _menuService.GetMenuItemsAsync();

            // Group items by category for easier rendering
            var groupedMenu = menuItems
                .GroupBy(item => item.Category)
                .ToDictionary(group => group.Key, group => group.ToList());

            return View(groupedMenu);
        }
    }
}
