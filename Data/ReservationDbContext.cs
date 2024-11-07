using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Models;

namespace ReservationSystem.Data;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
    {

    }

    public DbSet<Guest> Guests { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Sitting> Sittings { get; set; }
    public DbSet<RestaurantTable> RestaurantTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the relationship between Reservation and Table
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.RestaurantTable)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.RestaurantTableId);

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
    }
}
