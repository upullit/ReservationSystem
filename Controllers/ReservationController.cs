using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ReservationSystem.Data;
using ReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Controllers
{

    public class ReservationController : Controller
    {
        private readonly ReservationDbContext _context;
        public ReservationController(ReservationDbContext context)
        {
            _context = context;
        }

        // GET: ReservationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReservationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // Add a new reservation to databased based on form reservation data from the booking view
        // can redirect to Thanks for booking view after booking
        [HttpPost]
        public async Task<IActionResult> AddBooking(string guestName, int partiSize, DateTime date,
        string sittingTime, string restaurantArea, string phone, string email, string specialRequests)
        {
            if (ModelState.IsValid)
            {
                // create or find guest by phone and email
                var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Phone == phone && g.Email == email);
                if (guest == null)
                {
                    guest = new Guest { Name = guestName, Email = email, Phone = phone };
                    _context.Guests.Add(guest);
                    await _context.SaveChangesAsync();
                }

                // check sitting

                // Create the reservation
                var reservation = new Reservation
                {
                    GuestId = guest.Id,
                    //SittingId = sitting.Id,
                    RestaurantTableId = 1, // needs logic to select a table if iteration of avaliable
                    ReservationStatus = "Pending",
                    SpecialRequests = specialRequests,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return View("~/Views/Home/Booking.cshtml");
                //return RedirectToAction("ThanksBooking");
            }
            return View("~/Views/Home/Booking.cshtml");
            //return View("Booking");
        }

        // GET: /Home/ViewBookings
        // Retrieves all bookings from the database and displays them
        // This will be an admin staff features
        public async Task<IActionResult> AllReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();
            // Needs logic for how to display them
            return View(reservations);
        }

        // Get: /Home/EditReservation/{id}
        // Updates the reservation details based on form data from the EditReservation view
        [HttpPost]
        public async Task<IActionResult> EditReservations(int id, Reservation updatedReservation)
        {
            var reservation = await _context.Reservations.Include(r => r.Guest).FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            // Update the reservation details
            reservation.UpdatedAt = DateTime.Now;

            // Update Guest props
            if (reservation.Guest != null)
            {
                //reservation.Guest.Name = Booking.Name;
                //reservation.Guest.Phone = EditReservation.Phone;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Booking");
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
