using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;
using System.Threading.Tasks;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class SittingController : Controller
    {
        private readonly ReservationDbContext _context;

        public SittingController(ReservationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting == null)
            {
                return NotFound();
            }

            ViewBag.SittingTypes = new SelectList(_context.SittingTypes, "Id", "Name", sitting.SittingTypeId);
            return View(sitting);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Sitting updatedSitting)
        {
            if (id != updatedSitting.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(updatedSitting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SittingTypes = new SelectList(_context.SittingTypes, "Id", "Name", updatedSitting.SittingTypeId);
            return View(updatedSitting);
        }

        public IActionResult ReservationManagement()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.SittingTypes = new SelectList(_context.SittingTypes, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var sittings = await _context.Sittings
                                          .Include(s => s.SittingType)
                                          .OrderBy(s => s.StartTime)
                                          .ToListAsync();
            return View(sittings);
        }

        // Post Sitting create
        [HttpPost]
        public async Task<IActionResult> Create(Sitting newSitting)
        {
            //Console.WriteLine($"SittingTypeId: {newSitting.SittingTypeId}");
            //Console.WriteLine($"StartTime: {newSitting.StartTime}");
            //Console.WriteLine($"EndTime: {newSitting.EndTime}");
            //Console.WriteLine($"MaxCapacity: {newSitting.MaxCapacity}");
            if (ModelState.IsValid)
            {
                // Makes sure SittingType is loaded from the database
                newSitting.SittingType = await _context.SittingTypes.FindAsync(newSitting.SittingTypeId);

                _context.Sittings.Add(newSitting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}");
                    foreach (var err in error.Value.Errors)
                    {
                        Console.WriteLine($"Error: {err.ErrorMessage}");
                    }
                }
            }

            //Reload SittingTypes if the form submission fails validation
            ViewBag.SittingTypes = new SelectList(_context.SittingTypes, "Id", "Name");
            return View("Create", newSitting); // Ensure the view is re-rendered properly
        }

        // Delete Sitting
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var sitting = await _context.Sittings
                .Include(s => s.SittingType)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sitting == null)
            {
                return NotFound();
            }
            return View(sitting);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting != null)
            {
                _context.Sittings.Remove(sitting);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
