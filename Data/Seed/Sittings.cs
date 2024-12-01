using ReservationSystem.Models;

namespace ReservationSystem.Data.Seed
{
    public class Sittings
    {
        public static void Seed(ReservationDbContext context)
        {
            // Ensure SittingTypes are seeded first
            if (!context.SittingTypes.Any())
            {
                var sittingTypes = new List<SittingType>
                {
                    new SittingType { Name = "Breakfast" },
                    new SittingType { Name = "Lunch" },
                    new SittingType { Name = "Dinner" }
                };

                context.SittingTypes.AddRange(sittingTypes);
                context.SaveChanges();
            }

            // Fetch SittingType IDs
            var breakfastType = context.SittingTypes.First(st => st.Name == "Breakfast").Id;
            var lunchType = context.SittingTypes.First(st => st.Name == "Lunch").Id;
            var dinnerType = context.SittingTypes.First(st => st.Name == "Dinner").Id;

            // Check if there are already sittings in the database
            if (!context.Sittings.Any())
            {
                var sittings = new List<Sitting>();

                // Generate sittings for November and December
                for (var date = new DateTime(2024, 11, 1); date <= new DateTime(2024, 12, 31); date = date.AddDays(1))
                {
                    sittings.Add(new Sitting
                    {
                        StartTime = date.AddHours(7),
                        EndTime = date.AddHours(11).AddMinutes(30),
                        SittingTypeId = breakfastType,
                        MaxCapacity = 100
                    });

                    sittings.Add(new Sitting
                    {
                        StartTime = date.AddHours(12),
                        EndTime = date.AddHours(15).AddMinutes(30),
                        SittingTypeId = lunchType,
                        MaxCapacity = 100
                    });

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