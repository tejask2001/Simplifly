using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByBookingIdAsync(int bookingId);
    }
}
