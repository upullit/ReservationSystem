namespace ReservationSystem.Models
{
    public class Sitting
    {
        public int Id { get; set; } // Primary Key
        public string Type { get; set; } // Type of sitting (Breakfast, Lunch, Dinner)
        public TimeSpan StartTime { get; set; } // Start time for the sitting period
        public TimeSpan EndTime { get; set; } // End time for the sitting period
        public int MaxCapacity { get; set; } // Maximum number of guests allowed for the sitting

        // Navigation Properties
        // Navigation Properties
        public ICollection<Reservation> Reservations { get; set; }
    }
}
