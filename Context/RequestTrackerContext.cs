using Microsoft.EntityFrameworkCore;
using Simplifly.Models;

namespace Simplifly.Context
{
    public class RequestTrackerContext:DbContext
    {
        public RequestTrackerContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Airport> Airports { get; set; }        
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightOwner> FlightsOwner { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Models.Route> Route { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet <SeatDetail> Seats { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
