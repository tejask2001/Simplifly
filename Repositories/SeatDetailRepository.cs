using Simplifly.Context;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class SeatDetailRepository : IRepository<string, SeatDetail>
    {
        RequestTrackerContext _context;
        public SeatDetailRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public Task<SeatDetail> Add(SeatDetail items)
        {
            throw new NotImplementedException();
        }

        public Task<SeatDetail> Delete(SeatDetail items)
        {
            throw new NotImplementedException();
        }

        public Task<SeatDetail> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<SeatDetail>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SeatDetail> Update(SeatDetail items)
        {
            throw new NotImplementedException();
        }
    }
}
