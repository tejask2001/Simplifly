﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class PassengerBookingRepository : IRepository<int,PassengerBooking>,IPassengerBookingRepository
    {
        RequestTrackerContext _context;

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public PassengerBookingRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add PassengerBooking to the database
        /// </summary>
        /// <param name="items">Object of PassengerBooking</param>
        /// <returns>PassengerBooking object</returns>
        public async Task<Models.PassengerBooking> Add(Models.PassengerBooking items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        /// <summary>
        /// Method to delete PassengerBooking from database
        /// </summary>
        /// <param name="items">Object of PassengerBooking</param>
        /// <returns>PassengerBooking object</returns>
        /// <exception cref="NoSuchPassengerBookingException">throws exception if no PassengerBooking found</exception>
        public async Task<Models.PassengerBooking> Delete(int passengerBookingId)
        {
            var passengerBooking = await GetAsync(passengerBookingId);
            if (passengerBooking != null)
            {
                _context.Remove(passengerBooking);
                _context.SaveChanges();
                return passengerBooking;
            }
            throw new NoSuchPassengerBookingException();
        }

        /// <summary>
        /// Method to get PassengerBooking data of specific Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>PassengerBooking Object</returns>
        /// <exception cref="NoSuchPassengerBookingException">throws exception if no PassengerBooking found.</exception>
        public async Task<Models.PassengerBooking> GetAsync(int key)
        {
            var passengerBookings = await GetAsync();
            var passengerBooking = passengerBookings.FirstOrDefault(e => e.Id == key);
            if (passengerBooking != null)
            {
                return passengerBooking;
            }
            throw new NoSuchPassengerBookingException();
        }

        /// <summary>
        /// Method to get list of PassengerBooking
        /// </summary>
        /// <returns>PassengerBooking object</returns>
        public async Task<List<Models.PassengerBooking>> GetAsync()
        {
            var passengerBookings = _context.PassengerBookings.ToList();
            return passengerBookings;
        }

        public async Task AddPassengerBookingAsync(PassengerBooking passengerBooking)
        {
            _context.PassengerBookings.Add(passengerBooking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PassengerBooking>> GetPassengerBookingsAsync(int bookingId)
        {
            return await _context.PassengerBookings
                .Where(pb => pb.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task RemovePassengerBookingAsync(int passengerBookingId)
        {
            var passengerBooking = await _context.PassengerBookings.FindAsync(passengerBookingId);
            if (passengerBooking != null)
            {
                _context.PassengerBookings.Remove(passengerBooking);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method to update PassengerBooking.
        /// </summary>
        /// <param name="items">Object of PassengerBooking</param>
        /// <returns>Airport Object</returns>
        /// <exception cref="NoSuchPassengerBookingException">throws exception if no PassengerBooking found</exception</exception>
        public async Task<Models.PassengerBooking> Update(Models.PassengerBooking items)
        {
            var passengerBooking = await GetAsync(items.Id);
            if (passengerBooking != null)
            {
                _context.Entry<Models.PassengerBooking>(passengerBooking).State = EntityState.Modified;
                _context.SaveChanges();
                return passengerBooking;
            }
            throw new NoSuchPassengerBookingException();
        }
    }
}