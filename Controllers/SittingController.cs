using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers
{
    public class SittingController : Controller
    {
        private readonly ReservationDbContext _context;

        public SittingController(ReservationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Get Sitting / create
        public IActionResult Create()
        {
            return View();
        }

        // Post Sitting create
        [HttpPost]
        public async Task<IActionResult> Create(Sitting newSitting)
        {
            if (ModelState.IsValid)
            {
                _context.Sittings.Add(newSitting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newSitting);
        }

        // get sitting index
        //// GET: Sitting/Index
        //public async Task<IActionResult> Index()
        //{
        //    var sittings = await _context.Sittings.ToListAsync();
        //    return View(sittings);
        //}

        // Edit sitting

        // Delete Sitting
    }
}
