using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Data;
using ReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using ReservationSystem.Areas.User.Services;

namespace ReservationSystem.Areas.User.Controllers
{
    [Area("User")]
    public class ReservationController : Controller
    {
        private readonly ReservationService _reservationService;
        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        public ActionResult ThanksBooking()
        {
            return View();
        }

        // GET: ReservationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReservationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _reservationService.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation); // Pass the reservation to the view
        }

        public IActionResult Create()
        {
            ViewBag.OpeningTimes = ReservationService.OpeningTimes;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string guestName, int partySize, DateTime date,
        string timeSlot, string sittingArea, string phone, string email, string? specialRequests)
        {
            if (ModelState.IsValid)
            {
                // Parse the selected timeSlot
                if (!DateTime.TryParseExact(timeSlot, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime))
                {
                    ModelState.AddModelError("", "Invalid time slot selected.");
                    return View();
                }

                // Combine date and time into a single DateTime object
                var selectedDateTime = date.Date + parsedTime.TimeOfDay;

                // Find the sitting that matches the selected time
                var sitting = await _reservationService.FindSittingForTime(selectedDateTime);
                if (sitting == null)
                {
                    ModelState.AddModelError("", "No sitting matches the selected time.");
                    return View();
                }

                // Check if the sitting can accommodate the party size
                if (!await _reservationService.CanAccommodatePartySize(sitting.Id, partySize))
                {
                    string sittingName = sitting.SittingType?.Name ?? "this sitting"; // Fallback if SittingType.Name is null
                    string formattedDate = selectedDateTime.ToString("MMMM dd, yyyy"); // Format the date nicely
                    ModelState.AddModelError("", $"No more bookings are available for {sittingName} on {formattedDate}.");
                    ViewBag.ErrorMessage = $"No more bookings are available for {sittingName} on {formattedDate}.";
                    ViewBag.OpeningTimes = ReservationService.OpeningTimes; // Reload the time slots
                    return View();
                }

                // Find or create the guest
                var guest = await _reservationService.FindOrCreateGuest(guestName, phone, email);

                // Get the area prefix
                var areaPrefix = _reservationService.GetAreaPrefix(sittingArea);

                // Dynamically assign a table number
                var tableNumber = _reservationService.GetNextTableNumber(sitting.Id, areaPrefix);

                // Create the reservation
                var reservation = new Reservation
                {
                    GuestId = guest.Id,
                    SittingId = sitting.Id,
                    TableNumber = tableNumber,
                    ReservationStatus = "Pending",
                    SpecialRequests = specialRequests ?? string.Empty,
                    BookingTime = selectedDateTime,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PartySize = partySize
                };

                // Save the reservation
                try
                {
                    await _reservationService.SaveReservation(reservation);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to save the reservation. " + ex.Message);
                    return View();
                }

                return RedirectToAction("ThanksBooking");
            }

            // Reload form data if validation fails
            ViewBag.OpeningTimes = ReservationService.OpeningTimes;
            return View();
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationController/Edit/
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Reservation updatedReservation)
        {
            try
            {
                var reservation = await _reservationService.GetReservationById(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                // Update reservation fields
                reservation.PartySize = updatedReservation.PartySize;
                reservation.SpecialRequests = updatedReservation.SpecialRequests;
                reservation.UpdatedAt = DateTime.UtcNow;

                await _reservationService.SaveReservation(reservation);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Delete logic here if needed
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}