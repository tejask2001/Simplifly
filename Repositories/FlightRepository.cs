using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class FlightRepository : IRepository<string, Flight>
    {
        RequestTrackerContext _context;
        public FlightRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<Flight> Add(Flight items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        public async Task<Flight> Delete(Flight items)
        {
            var flight = await GetAsync(items.FlightNumber);
            if (flight != null)
            {
                _context.Remove(flight);
                _context.SaveChanges();
                return flight;
            }
            throw new NoSuchFlightException();
        }

        public async Task<Flight> GetAsync(string key)
        {
            var flights= await GetAsync();
            var flight= flights.FirstOrDefault(e=>e.FlightNumber==key);
            if (flight != null)
            {
                return flight;
            }
            throw new NoSuchFlightException();
        }

        public async Task<List<Flight>> GetAsync()
        {
            var flights = _context.Flights.ToList();
            return flights;
        }

        public async Task<Flight> Update(Flight items)
        {
            var flight = await GetAsync(items.FlightNumber);
            if(flight!= null)
            {
                _context.Entry<Flight>(items).State = EntityState.Modified;
                _context.SaveChanges();
                return flight;
            }
            throw new NoSuchFlightException();
        }
    }
}
