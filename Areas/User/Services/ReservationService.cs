using ReservationSystem.Data;
using ReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Areas.User.Services;

public class ReservationService
{
    private readonly ReservationDbContext _context;

    public ReservationService(ReservationDbContext context)
    {
        _context = context;
    }

    // Private list of opening times
    public static readonly List<string> OpeningTimes = new List<string>
        {
            "07:00 AM", "07:30 AM", "08:00 AM", "08:30 AM",
            "09:00 AM", "09:30 AM", "10:00 AM", "10:30 AM",
            "11:00 AM", "11:30 AM", "12:00 PM", "12:30 PM",
            "01:00 PM", "01:30 PM", "02:00 PM", "02:30 PM",
            "03:00 PM", "03:30 PM", "05:00 PM", "05:30 PM",
            "06:00 PM", "06:30 PM", "07:00 PM", "07:30 PM",
            "08:00 PM", "08:30 PM", "09:00 PM", "09:30 PM"
        };

    public async Task<bool> CanAccommodatePartySize(int sittingId, int partySize)
    {
        var totalPartySize = await _context.Reservations
            .Where(r => r.SittingId == sittingId)
            .SumAsync(r => r.PartySize ?? 0);

        var sitting = await _context.Sittings.FirstOrDefaultAsync(s => s.Id == sittingId);
        var maxCapacity = sitting?.MaxCapacity ?? 100;

        return (totalPartySize + partySize) <= maxCapacity;
    }

    public string GetAreaPrefix(string area)
    {
        return area switch
        {
            "Indoor Restaurant" => "M",
            "Outdoor" => "O",
            "Bar Area" => "B",
            _ => "A" // Default to "A" for unknown areas
        };
    }

    public string GetNextTableNumber(int sittingId, string area)
    {
        var reservations = _context.Reservations
            .Where(r => r.SittingId == sittingId)
            .ToList();

        var areaReservations = reservations
            .Where(r => r.TableNumber.StartsWith(area))
            .Select(r => r.TableNumber)
            .ToList();

        var highestNumber = areaReservations
            .Select(t => int.Parse(t.Substring(1))) // Extract the number part (e.g., "A1" -> 1)
            .DefaultIfEmpty(0)
            .Max();

        return $"{area}{highestNumber + 1}";
    }

    // New: Find the sitting for a specific time
    public async Task<Sitting?> FindSittingForTime(DateTime selectedDateTime)
    {
        return await _context.Sittings.FirstOrDefaultAsync(s =>
            s.StartTime.Date == selectedDateTime.Date &&
            s.StartTime.TimeOfDay <= selectedDateTime.TimeOfDay &&
            s.EndTime.TimeOfDay > selectedDateTime.TimeOfDay);
    }

    // New: Find or create a guest
    public async Task<Guest> FindOrCreateGuest(string guestName, string phone, string email)
    {
        var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Phone == phone && g.Email == email);
        if (guest == null)
        {
            guest = new Guest { Name = guestName, Email = email, Phone = phone };
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }
        return guest;
    }

    // New: Save the reservation to the database
    public async Task SaveReservation(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
    }

    // Get all reservations
    public async Task<List<Reservation>> GetAllReservations()
    {
        return await _context.Reservations
            .Include(r => r.Guest)
            .Include(r => r.Sitting)
            .ToListAsync();
    }

    // Get Reservation by ID
    public async Task<Reservation?> GetReservationById(int id)
    {
        return await _context.Reservations
            .Include(r => r.Guest) // Include related guest data
            .Include(r => r.Sitting) // Include related sitting data
            .FirstOrDefaultAsync(r => r.Id == id);
    }
    public async Task<bool> DeleteReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return false;
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return true;
    }
}