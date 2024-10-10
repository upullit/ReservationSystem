namespace ReservationSystem.Models;

public class Reservation
{
    public int Id { get; set; } // Primary Key
    public int GuestId { get; set; } // Foreign Key
    public int SittingId { get; set; } // Foreign Key
    public int TableId { get; set; } // Foreign Key
    public string ReservationStatus { get; set; } // Status of the reservation (Pending, Confirmed, etc.)
    public string SpecialRequests { get; set; } // Additional requests or preferences from the guest
    public DateTime CreatedAt { get; set; } // Date and time the reservation was created
    public DateTime UpdatedAt { get; set; } // Date and time the reservation was last updated

    // Navigation Properties
    public Guest Guests { get; set; }
    public Sitting Sittings { get; set; }
    public Table Tables { get; set; }
}
