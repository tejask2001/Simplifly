using Microsoft.EntityFrameworkCore;
using Simplifly.Models;
using System.Drawing;

namespace Simplifly.Context
{
    public class RequestTrackerContext:DbContext
    {

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightOwner> FlightsOwner { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<PassengerBooking> PassengerBookings { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<SeatDetail> Seats { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        public RequestTrackerContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed
                                                  // Configure the Route entity relationships
            modelBuilder.Entity<Models.Route>()
                .HasOne(r => r.SourceAirport)
                .WithMany()
                .HasForeignKey(r => r.SourceAirportId)
                .OnDelete(DeleteBehavior.Restrict); // Remove ON DELETE CASCADE

            modelBuilder.Entity<Models.Route>()
                .HasOne(r => r.DestinationAirport)
                .WithMany()
                .HasForeignKey(r => r.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict); // Remove ON DELETE CASCADE



            base.OnModelCreating(modelBuilder);
        }

      

    }
}
