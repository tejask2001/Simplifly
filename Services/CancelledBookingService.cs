using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Services
{
    public class CancelledBookingService : ICancelledBookingService
    {
        private readonly IRepository<int,CancelledBooking> _cancelledBookingRepository;
        private readonly IRepository<int, Payment> _paymentRepository;
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<int, Customer> _customerRepository;

        private readonly ILogger<CancelledBookingService> _logger;

        PaymentDetails _paymentDetails;
        CancelledBooking cancelledBooking;

        public CancelledBookingService(IRepository<int, CancelledBooking> cancelledBookingRepository, IRepository<int, Booking> bookingRepository , IRepository<int, Payment> paymentRepository, IRepository<int, Customer> customerRepository, ILogger<CancelledBookingService> logger)
        {
            _cancelledBookingRepository = cancelledBookingRepository;
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _logger = logger;

            _paymentDetails = new PaymentDetails();
            cancelledBooking = new CancelledBooking();
        }
        public async Task<CancelledBooking> AddCancelledBooking(Booking booking, PassengerBooking passengerBooking, double refundAmount)
        {
            var payment = await _paymentRepository.GetAsync(booking.PaymentId);

            cancelledBooking.passengerName = passengerBooking.Passenger.Name;
            cancelledBooking.scheduleId = booking.ScheduleId;
            cancelledBooking.paymentId = booking.PaymentId;
            cancelledBooking.RefundAmount=refundAmount;
            cancelledBooking.RefundStatus = "pending";
            cancelledBooking.cardNumber = payment.PaymentDetails.CardNumber;
            cancelledBooking.UserId = booking.UserId;

            booking.TotalPrice = booking.TotalPrice - refundAmount;
            var updateBooking = await _bookingRepository.Update(booking);            

            var cancelBooking  = await _cancelledBookingRepository.Add(cancelledBooking);
            if(cancelBooking != null)
            {
                return cancelledBooking;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<CancelledBooking> AddCancelledBooking(int bookingId)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);

            var user = await _customerRepository.GetAsync(booking.UserId);

            cancelledBooking.passengerName = user.Name;
            cancelledBooking.scheduleId = booking.ScheduleId;
            cancelledBooking.paymentId = booking.PaymentId;
            cancelledBooking.RefundAmount= booking.TotalPrice;
            cancelledBooking.RefundStatus = "pending";
            cancelledBooking.cardNumber = "";
            cancelledBooking.UserId = booking.UserId;

            await _bookingRepository.Delete(bookingId);

            var cancelBooking = await _cancelledBookingRepository.Add(cancelledBooking);
            if (cancelBooking != null)
            {
                return cancelledBooking;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<List<CancelledBooking>> GetCancelledBooking()
        {
            var bookings = await _cancelledBookingRepository.GetAsync();
            if(bookings!= null)
            {
                return bookings;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<List<CancelledBooking>> GetCancelledBookingByUserId(int userId)
        {
            var bookings=await _cancelledBookingRepository.GetAsync();
            bookings=bookings.Where(e=>e.UserId==userId).ToList();
            if (bookings != null)
            {
                return bookings;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<List<CancelledBooking>> GetRefundRequest()
        {
            var refundRequest = await _cancelledBookingRepository.GetAsync();
            refundRequest = refundRequest.Where(e => e.RefundStatus == "request sent").ToList();
            if(refundRequest != null)
            {
                return refundRequest;
            }
            throw new NoCancelledBookingFound();
        }

        public async Task<CancelledBooking> UpdateCancelledBooking(int id)
        {
            var cancelBooking=await _cancelledBookingRepository.GetAsync(id);
            if(cancelBooking!= null)
            {
                if (cancelBooking.RefundStatus == "pending")
                {
                    cancelBooking.RefundStatus = "request sent";
                    cancelBooking=await _cancelledBookingRepository.Update(cancelBooking);
                    return cancelBooking;
                }
                else if (cancelBooking.RefundStatus == "request sent") ;
                {
                    cancelBooking.RefundStatus = "amount refunded";
                    cancelBooking = await _cancelledBookingRepository.Update(cancelBooking);
                    return cancelBooking;
                }
            }
            throw new NoCancelledBookingFound();
        }
    }
}
