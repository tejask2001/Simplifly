using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface ISeatDeatilRepository
    {
        Task<IEnumerable<SeatDetail>> GetSeatDetailsAsync(List<int> seatIds);
        Task UpdateSeatDetailsAsync(IEnumerable<SeatDetail> seatDetails);
    }
}
