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
    public DbSet<Table> Tables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the relationship between Reservation and Table
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Tables)
            .WithMany()
            .HasForeignKey(r => r.TableId);

        // Configure the relationship between Reservation and Guest
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Guests)
            .WithMany(g => g.Reservations)
            .HasForeignKey(r => r.GuestId);

        // Configure the relationship between Reservation and Sitting
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Sittings)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SittingId);
    }
}
