using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface ICancelledBookingService
    {
        public Task<CancelledBooking> AddCancelledBooking(Booking booking, PassengerBooking passengerBooking,double refundAmount);
        public Task<CancelledBooking> AddCancelledBooking(int bookingId);
        public Task<List<CancelledBooking>> GetCancelledBooking();
        public Task<List<CancelledBooking>> GetCancelledBookingByUserId(int userId);
        public Task<List<CancelledBooking>> GetRefundRequest();
        public Task<CancelledBooking> UpdateCancelledBooking(int id);
    }
}
