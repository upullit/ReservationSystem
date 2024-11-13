namespace ReservationSystem.Models;

public class Reservation
{
    public int Id { get; set; } // Primary Key
    public int GuestId { get; set; } // Foreign Key
    public int SittingId { get; set; } // Foreign Key
    public string TableNumber { get; set; } = string.Empty; // will be Dynamically assigned table numbers through the logic in the program
    public string ReservationStatus { get; set; } = string.Empty; // Status of the reservation (Pending, Confirmed, etc.)
    public string? SpecialRequests { get; set; } = string.Empty; // Additional requests or preferences from the guest
    public int? PartySize { get; set; }
    public DateTime BookingTime { get; set; } // Date and time the reservation is sched
    public DateTime CreatedAt { get; set; } // Date and time the reservation was created
    public DateTime UpdatedAt { get; set; } // Date and time the reservation was last updated

    // Navigation Properties
    public Guest? Guest { get; set; }
    public Sitting? Sitting { get; set; }
}
