namespace ReservationSystem.Models
{
    public class Sitting
    {
        public int Id { get; set; } // Primary Key
        public int SittingTypeId { get; set; }
        public DateTime StartTime { get; set; } // Start time for the sitting period
        public DateTime EndTime { get; set; } // End time for the sitting period
        public int MaxCapacity { get; set; } // Maximum number of guests allowed for the sitting

        public SittingType SittingType { get; set; } = default!;

        // Navigation Properties
        // Navigation Properties
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
