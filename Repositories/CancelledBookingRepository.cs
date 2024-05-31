using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class CancelledBookingRepository : IRepository<int, CancelledBooking>
    {
        private readonly RequestTrackerContext _context;
        private readonly ILogger<CancelledBookingRepository> _logger;

        public CancelledBookingRepository(RequestTrackerContext context,ILogger<CancelledBookingRepository> logger)
        {
            _context = context;
            _logger=logger;
        }
        public async Task<CancelledBooking> Add(CancelledBooking items)
        {
            _context.Add(items);
            _context.SaveChanges();
            _logger.LogInformation("cancelledBooking added");
            return items;
        }

        public async Task<CancelledBooking> Delete(int key)
        {
            var cancelledBooking=await GetAsync(key);
            if (cancelledBooking != null)
            {
                _context.Remove(cancelledBooking);
                _context.SaveChanges();
                _logger.LogInformation("cancelledBooking removed");
                return cancelledBooking;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<CancelledBooking> GetAsync(int key)
        {
            var cancelledBookings = await GetAsync();
            var cancelledBooking = cancelledBookings.FirstOrDefault(e => e.Id == key);
            if(cancelledBooking != null)
            {
                return cancelledBooking;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<List<CancelledBooking>> GetAsync()
        {
            var cancelledBookings = _context.CancelledBookings.Include(e=>e.Schedule)
                .Include(e=>e.Schedule.Route).Include(e=>e.Schedule.Route.SourceAirport)
                .Include(e => e.Schedule.Route.DestinationAirport).ToList();
            if (cancelledBookings != null)
            {
                return cancelledBookings;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<CancelledBooking> Update(CancelledBooking items)
        {
            var cancelledBooking = await GetAsync(items.Id);
            if (cancelledBooking != null)
            {
                _context.Entry<CancelledBooking>(items).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation("cancelledBooking updated");
                return cancelledBooking;
            }
            throw new NoCancelledBookingFound();
        }
    }
}
