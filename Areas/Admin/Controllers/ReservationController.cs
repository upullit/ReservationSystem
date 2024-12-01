using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.User.Services;
using ReservationSystem.Data;
using ReservationSystem.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ReservationSystem.Areas.Admin.Controllers;

[Area("Admin")]
public class ReservationController : Controller
{
    private readonly ReservationService _reservationService;
    private readonly ReservationDbContext _context;

    public ReservationController(ReservationService reservationService, ReservationDbContext context)
    {
        _reservationService = reservationService;
        _context = context;
    }

    // GET: Admin/Reservation/Index Sort by Date and status = Pending
    public async Task<IActionResult> Index(string status = "Pending", DateTime? date = null)
    {

        // For Completed, don't filter by date unless explicitly provided
        if (status != "Completed")
        {
            date ??= DateTime.UtcNow.Date.AddDays(1); // Default to tomorrow for Pending/Confirmed
        }

        var reservationsBySitting = await _reservationService.GetReservationsGroupedBySitting(status, date);

        ViewBag.SelectedDate = date?.ToString("yyyy-MM-dd") ?? string.Empty; // Keep empty for Completed
        ViewBag.Status = status; // Keeps current pending or confirmed or completed status for date changes in filter search
        return View(reservationsBySitting);
    }

    // GET: Admin/Reservation/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var reservation = await _reservationService.GetReservationById(id);
        if (reservation == null)
        {
            return NotFound();
        }
        return View(reservation);
    }

    // GET: Admin/Reservation/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var reservation = await _reservationService.GetReservationById(id);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // POST: Admin/Reservation/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var success = await _reservationService.DeleteReservation(id);

        if (!success)
        {
            TempData["ErrorMessage"] = "Failed to delete the reservation. Please try again.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Reservation deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Reservation/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var reservation = await _reservationService.GetReservationById(id);
        if (reservation == null)
        {
            return NotFound();
        }

        // Populate ViewBag with guests for dropdown
        ViewBag.Guests = new SelectList(await _context.Guests.ToListAsync(), "Id", "Name", reservation.GuestId);

        return View(reservation);
    }

    // POST: Admin/Reservation/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Reservation updatedReservation)
    {
        if (id != updatedReservation.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var success = await _reservationService.UpdateReservation(updatedReservation);
            if (success)
            {
                TempData["SuccessMessage"] = "Reservation updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to update reservation. Please try again.";
        }

        // Reload ViewBag with guests if validation fails
        ViewBag.Guests = new SelectList(await _context.Guests.ToListAsync(), "Id", "Name", updatedReservation.GuestId);

        return View(updatedReservation);
    }

    // POST: Admin/Reservation/ChangeStatus/5
    [HttpPost]
    public async Task<IActionResult> ChangeStatus(int id, string newStatus)
    {
        var success = await _reservationService.ChangeReservationStatus(id, newStatus);
        if (!success)
        {
            TempData["ErrorMessage"] = "Failed to change reservation status.";
        }
        else
        {
            TempData["SuccessMessage"] = "Reservation status updated successfully.";
        }
        return RedirectToAction(nameof(Index));
    }
}
