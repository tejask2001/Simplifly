using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class AirportRepository : IRepository<int, Airport>
    {
        readonly RequestTrackerContext _context;
        public AirportRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<Airport> Add(Airport items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        public Task<Airport> Delete(Airport items)
        {
            var airport = GetAsync(items.Id);
            if (airport != null)
            {
                _context.Remove(airport);
                _context.SaveChanges();
                return airport;
            }
            throw new NoSuchAirportException();
        }

        public async Task<Airport> GetAsync(int key)
        {
            var airports = await GetAsync();
            var airport=airports.FirstOrDefault(e=>e.Id==key);
            if(airport != null)
            {
                return airport;
            }
            throw new NoSuchAirportException();
        }

        public async Task<List<Airport>>? GetAsync()
        {
            var airports = _context.Airports.ToList();
            return airports;
        }

        public async Task<Airport> Update(Airport items)
        {
            var airport = await GetAsync(items.Id);
            _context.Entry<Airport>(items).State=EntityState.Modified;
            _context.SaveChanges();
            return airport;
        }
    }
}
