using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace ReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class SittingTypeController : Controller
    {
        private readonly ReservationDbContext _context;

        public SittingTypeController(ReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sittingTypes = await _context.SittingTypes.ToListAsync();
            return View(sittingTypes);
        }

        // GET: SittingType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SittingType/Create
        [HttpPost]
        public async Task<IActionResult> CreateSittingType(SittingType sittingType)
        {
            if (ModelState.IsValid)
            {
                _context.SittingTypes.Add(sittingType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sittingType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sittingType = await _context.SittingTypes.FindAsync(id);
            if (sittingType == null)
            {
                return NotFound();
            }
            return View(sittingType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SittingType updatedSittingType)
        {
            if (id != updatedSittingType.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(updatedSittingType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(updatedSittingType);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var sittingType = await _context.SittingTypes.FindAsync(id);
            if (sittingType == null)
            {
                return NotFound();
            }
            return View(sittingType);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sittingType = await _context.SittingTypes.FindAsync(id);
            if (sittingType != null)
            {
                _context.SittingTypes.Remove(sittingType);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
