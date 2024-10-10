namespace ReservationSystem.Models;

public class Guest
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; } // Name of the guest
    public string Contact { get; set; } // Contact information (phone or email)

    // Navigation Properties
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

}
