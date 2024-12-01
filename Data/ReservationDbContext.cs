using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;

namespace ReservationSystem.Data
{
    public class ReservationDbContext : IdentityDbContext<IdentityUser>
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
        {
        }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<SittingType> SittingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important: Call the base method for Identity configurations

            // Configure the relationship between Reservation and Guest
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(r => r.GuestId);

            // Configure the relationship between Reservation and Sitting
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Sitting)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SittingId);

            modelBuilder.Entity<Sitting>()
                .HasOne(s => s.SittingType)
                .WithMany()
                .HasForeignKey(s => s.SittingTypeId);

            modelBuilder.Entity<SittingType>()
                .Property(st => st.Name)
                .IsRequired();

            // Optional: Seed data for SittingType
            modelBuilder.Entity<SittingType>().HasData(
                new SittingType { Id = 1, Name = "Breakfast" },
                new SittingType { Id = 2, Name = "Lunch" },
                new SittingType { Id = 3, Name = "Dinner" });
        }
    }
}
