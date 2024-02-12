using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class SeatDetailRepository : IRepository<string, SeatDetail>,ISeatDeatilRepository
    {
        RequestTrackerContext _context;

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public SeatDetailRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add SeatDetail to the database
        /// </summary>
        /// <param name="items">Object of SeatDetail</param>
        /// <returns>SeatDetail object</returns>
        public async Task<SeatDetail> Add(SeatDetail items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        /// <summary>
        /// Method to delete SeatDetail from database
        /// </summary>
        /// <param name="items">Object of SeatDetail</param>
        /// <returns>SeatDetail object</returns>
        /// <exception cref="NoSuchSeatException">throws exception if no SeatDetail found</exception>
        public async Task<SeatDetail> Delete(string seatNumber)
        {
            var seatDetail = await GetAsync(seatNumber);
            if (seatDetail != null)
            {
                _context.Remove(seatDetail);
                _context.SaveChanges();
                return seatDetail;
            }
            throw new NoSuchSeatException();
        }

        /// <summary>
        /// Method to get SeatDetail data of specific Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>SeatDetail Object</returns>
        /// <exception cref="NoSuchSeatException">throws exception if no SeatDetail found.</exception>
        public async Task<SeatDetail> GetAsync(string key)
        {
            var seatDetails = await GetAsync();
            var seatDetail= seatDetails.FirstOrDefault(e=>e.SeatNumber==key);
            if (seatDetail!=null)
            {
                return seatDetail;
            }
            throw new NoSuchSeatException();
        }

        public async Task<IEnumerable<SeatDetail>> GetSeatDetailsAsync(List<int> seatIds)
        {
            return await Task.FromResult(_context.Seats.Where(s => seatIds.Contains(s.Id)).ToList());
        }

        public async Task UpdateSeatDetailsAsync(IEnumerable<SeatDetail> seatDetails)
        {
            _context.Seats.UpdateRange(seatDetails);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to get list of SeatDetail
        /// </summary>
        /// <returns>SeatDetail object</returns>
        public async Task<List<SeatDetail>> GetAsync()
        {
            var seatDetails= _context.Seats.ToList();
            return seatDetails;
        }

        /// <summary>
        /// Method to update SeatDetail.
        /// </summary>
        /// <param name="items">Object of SeatDetail</param>
        /// <returns>SeatDetail Object</returns>
        /// <exception cref="NoSuchSeatException">throws exception if no SeatDetail found</exception</exception>
        public async Task<SeatDetail> Update(SeatDetail items)
        {
            var seatDetail = await GetAsync(items.SeatNumber);
            if(seatDetail != null)
            {
                _context.Entry(seatDetail).State = EntityState.Modified;
                _context.SaveChanges();
                return seatDetail;
            }
            throw new NoSuchSeatException();
        }
    }
}
