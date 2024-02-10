using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;

namespace Simplifly.Repositories
{
    public class PassengerRepository : IRepository<int, Models.Passenger>
    {
        RequestTrackerContext _context;

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public PassengerRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add Passenger to the database
        /// </summary>
        /// <param name="items">Object of Passenger</param>
        /// <returns>Passenger object</returns>
        public async Task<Models.Passenger> Add(Models.Passenger items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        /// <summary>
        /// Method to delete Passenger from database
        /// </summary>
        /// <param name="items">Object of Passenger</param>
        /// <returns>Passenger object</returns>
        /// <exception cref="NoSuchPassengerException">throws exception if no Passenger found</exception>
        public Task<Models.Passenger> Delete(int passengerId)
        {
            var passenger = GetAsync(passengerId);
            if (passenger != null)
            {
                _context.Remove(passenger);
                _context.SaveChanges();
                return passenger;
            }
            throw new NoSuchPassengerException();
        }

        /// <summary>
        /// Method to get Passenger data of specific Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Passenger Object</returns>
        /// <exception cref="NoSuchPassengerException">throws exception if no Passenger found.</exception>
        public async Task<Models.Passenger> GetAsync(int key)
        {
            var passengers = await GetAsync();
            var passenger = passengers.FirstOrDefault(e => e.PassengerId == key);
            if (passenger != null)
            {
                return passenger;
            }
            throw new NoSuchPassengerException();
        }

        /// <summary>
        /// Method to get list of Passenger
        /// </summary>
        /// <returns>Passenger object</returns>
        public async Task<List<Models.Passenger>> GetAsync()
        {
            var passengers = _context.Passengers.ToList();
            return passengers;
        }

        /// <summary>
        /// Method to update Passenger.
        /// </summary>
        /// <param name="items">Object of Passenger</param>
        /// <returns>Airport Object</returns>
        /// <exception cref="NoSuchPassengerException">throws exception if no Passenger found</exception</exception>
        public async Task<Models.Passenger> Update(Models.Passenger items)
        {
            var passenger = await GetAsync(items.PassengerId);
            if (passenger != null)
            {
                _context.Entry<Models.Passenger>(passenger).State = EntityState.Modified;
                _context.SaveChanges();
                return passenger;
            }
            throw new NoSuchPassengerException();
        }
    }
}
