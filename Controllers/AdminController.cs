using Microsoft.AspNetCore.Mvc;

namespace ReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult SittingManagement()
        {
            return View();
        }
        public IActionResult ReservationManagement()
        {
            return View();
        }

    }

}
