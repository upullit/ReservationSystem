namespace ReservationSystem.Models;

public class Guest
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; } = string.Empty; // Name of the guest
    public string Phone { get; set; } = string.Empty; // Contact information (phone or email)
    public string Email { get; set; } = string.Empty; // Contact information (phone or email)

    // Navigation Properties
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
