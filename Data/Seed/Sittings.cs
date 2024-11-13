using ReservationSystem.Models;

namespace ReservationSystem.Data.Seed
{
    public class Sittings
    {
        public static void Seed(ReservationDbContext context)
        {
            // Check if there are already sittings in the database
            if (!context.Sittings.Any())
            {
                // Get SittingType Ids (fallback to default Ids if not found)
                var breakfastType = context.SittingTypes.FirstOrDefault(st => st.Name == "Breakfast")?.Id ?? 1;
                var lunchType = context.SittingTypes.FirstOrDefault(st => st.Name == "Lunch")?.Id ?? 2;
                var dinnerType = context.SittingTypes.FirstOrDefault(st => st.Name == "Dinner")?.Id ?? 3;

                // Create a list to hold sittings
                var sittings = new List<Sitting>();

                // Generate sittings for November and December
                for (var date = new DateTime(2024, 11, 1); date <= new DateTime(2024, 12, 31); date = date.AddDays(1))
                {
                    // Breakfast sitting (7:00 AM - 11:30 AM)
                    sittings.Add(new Sitting
                    {
                        StartTime = date.AddHours(7),
                        EndTime = date.AddHours(11).AddMinutes(30),
                        SittingTypeId = breakfastType,
                        MaxCapacity = 100
                    });

                    // Lunch sitting (12:00 PM - 3:30 PM)
                    sittings.Add(new Sitting
                    {
                        StartTime = date.AddHours(12),
                        EndTime = date.AddHours(15).AddMinutes(30),
                        SittingTypeId = lunchType,
                        MaxCapacity = 100
                    });

                    // Dinner sitting (5:00 PM - 9:30 PM)
                    sittings.Add(new Sitting
                    {
                        StartTime = date.AddHours(17),
                        EndTime = date.AddHours(21).AddMinutes(30),
                        SittingTypeId = dinnerType,
                        MaxCapacity = 100
                    });
                }

                // Add sittings to the context and save changes
                context.Sittings.AddRange(sittings);
                context.SaveChanges();
            }
        }
    }
}
