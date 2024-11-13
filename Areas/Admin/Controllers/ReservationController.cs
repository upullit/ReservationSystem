using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Areas.User.Services;

namespace ReservationSystem.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ReservationController : Controller
	{
		private readonly ReservationService _reservationService;

		public ReservationController(ReservationService reservationService)
		{
			_reservationService = reservationService;
		}

		// Get: Admin/Reservation/Index
		public async Task<IActionResult> Index()
		{
			var reservations = await _reservationService.GetAllReservations();
			return View(reservations);
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
        [HttpPost]
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
    }
}
