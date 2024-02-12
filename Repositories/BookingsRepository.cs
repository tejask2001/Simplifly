using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class BookingsRepository : IRepository<int, Booking>, IBookingRepository
    {
        RequestTrackerContext _context;

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public BookingsRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add Booking to the database
        /// </summary>
        /// <param name="items">Object of Booking</param>
        /// <returns>Booking object</returns>
        public async Task<Booking> Add(Booking items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        /// <summary>
        /// Method to delete Booking from database
        /// </summary>
        /// <param name="items">Object of Booking</param>
        /// <returns>Booking object</returns>
        /// <exception cref="NoSuchBookingsException">throws exception if no booking found</exception>
        public async Task<Booking> Delete(int bookingId)
        {
            var booking = await GetAsync(bookingId);
            if (booking != null)
            {
                _context.Remove(booking);
                _context.SaveChanges();
                return booking;
            }
            throw new NoSuchBookingsException();
        }

        /// <summary>
        /// Method to get Booking data of specific Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Booking Object</returns>
        /// <exception cref="NoSuchBookingsException">throws exception if no Booking found.</exception>
        public async Task<Booking> GetAsync(int key)
        {
            var bookings = await GetAsync();
            var booking= bookings.FirstOrDefault(e=>e.Id==key);
            if(booking!=null)
            {
                return booking;
            }
            throw new NoSuchBookingsException();
        }

        /// <summary>
        /// Method to get list of Booking
        /// </summary>
        /// <returns>Booking objects</returns>
        public async Task<List<Booking>> GetAsync()
        {
            var bookings = _context.Bookings.ToList();
            return bookings;
        }

        /// <summary>
        /// Method to update Booking.
        /// </summary>
        /// <param name="items">Object of Booking</param>
        /// <returns>Booking Object</returns>
        /// <exception cref="NoSuchBookingsException">throws exception if no Booking found</exception</exception>
        public async Task<Booking> Update(Booking items)
        {
            var bookings = await GetAsync(items.Id);
            if( bookings != null)
            {
                _context.Entry<Booking>(items).State = EntityState.Modified;
                _context.SaveChanges();
                return bookings;
            }
            throw new NoSuchBookingsException();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
        public async Task<bool> CheckSeatsAvailabilityAsync(string flightId, List<int> seatIds)
        {
            // Get all booked seats for the given flight
            var bookedSeats = await _context.PassengerBookings
                .Where(pb => pb.Booking.FlightId == flightId && seatIds.Contains(pb.SeatId ?? -1))
                .Select(pb => pb.SeatId)
                .ToListAsync();

            // Check if any of the requested seats are already booked
            foreach (var seatId in seatIds)
            {
                if (bookedSeats.Contains(seatId))
                {
                    // Seat is already booked, return false
                    return false;
                }
            }

            // All seats are available
            return true;
        }
    }
}
