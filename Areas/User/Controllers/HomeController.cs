using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Models;
using System.Diagnostics;

namespace ReservationSystem.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Booking()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult OrderOnline()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderOnline()
        {
            return View();
        }
    }
}
