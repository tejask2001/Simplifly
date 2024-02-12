using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class FlightOwnerRepository: IRepository<int, FlightOwner>
    {
        private readonly RequestTrackerContext _context;
        private readonly ILogger<FlightOwnerRepository> _logger;

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public FlightOwnerRepository(RequestTrackerContext context, ILogger<FlightOwnerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<FlightOwner> Add(FlightOwner items)
        {
            _context.Add(items);
            _context.SaveChanges();
            _logger.LogInformation("FlightOwner added " + items.OwnerId);
            return items;
        }

        public async Task<FlightOwner> Delete(int Id)
        {
            var owner = await GetAsync( Id);
            if (owner != null)
            {
                _context?.Remove(owner);
                _context.SaveChanges();
                return owner;
            }
            throw new NoSuchFlightOwnerException();
        }

        public async Task<FlightOwner> GetAsync(int key)
        {
            var flightOwners = await GetAsync();
            var flightOwner = flightOwners.FirstOrDefault(e => e.OwnerId == key);
            if (flightOwner != null)
            {
                return flightOwner;
            }
            throw new NoSuchFlightOwnerException();
        }

        public async Task<List<FlightOwner>> GetAsync()
        {
            var flightOwners = _context.FlightsOwners.ToList();
            return flightOwners;
        }

        public async Task<FlightOwner> Update(FlightOwner items)
        {
            var flightOwner = await GetAsync(items.OwnerId);
            
                _context.Entry<FlightOwner>(items).State = EntityState.Modified;
                _context.SaveChanges();
                return flightOwner;

        }
    }
}
