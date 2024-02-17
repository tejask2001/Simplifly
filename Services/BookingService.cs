using Simplifly.Controllers;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Repositories;

namespace Simplifly.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<string, Flight> _flightRepository;
        private readonly IRepository<int, PassengerBooking> _passengerBookingRepository;
        private readonly ISeatDeatilRepository _seatDetailRepository;
        private readonly IRepository<int, Schedule> _scheduleRepository;
        private readonly IPassengerBookingRepository _passengerBookingsRepository;
        private readonly IBookingRepository _bookingsRepository;

        private readonly IRepository<int, Payment> _paymentRepository;
      //  private readonly IPaymentRepository _paymentsRepository;

        private readonly ILogger<BookingService> _logger;
        public BookingService(IRepository<int, Booking> bookingRepository, IRepository<int, Schedule> scheduleRepository, IRepository<int, PassengerBooking> passengerBookingRepository, IRepository<string, Flight> flightRepository, IBookingRepository bookingsRepository, ISeatDeatilRepository seatDetailRepository, IPassengerBookingRepository passengerBookingRepository1, IRepository<int, Payment> paymentRepository, ILogger<BookingService> logger)
        {
            _flightRepository = flightRepository;
            _bookingsRepository = bookingsRepository;
            _passengerBookingsRepository = passengerBookingRepository1;
            _bookingRepository = bookingRepository;
            _passengerBookingRepository = passengerBookingRepository;
            _seatDetailRepository = seatDetailRepository;
            _scheduleRepository = scheduleRepository;
            _paymentRepository = paymentRepository;
            _logger = logger;
        }
        public async Task<bool> CreateBookingAsync(BookingRequestDto bookingRequest)
        {
            // Perform validation and business logic
            if (bookingRequest == null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            var isSeatsAvailable = await _passengerBookingsRepository.CheckSeatsAvailabilityAsync(bookingRequest.ScheduleId, bookingRequest.SelectedSeats);
            if (!isSeatsAvailable)
            {
                // Handle case where selected seats are not available
                return false;
            }

            var schedule = await _scheduleRepository.GetAsync(bookingRequest.ScheduleId);
            if (schedule == null)
            {
                throw new Exception("Schedule not found.");
            }

            // Calculate total price based on the number of selected seats
            var totalPrice = CalculateTotalPrice(bookingRequest.SelectedSeats.Count, await _flightRepository.GetAsync(schedule.FlightId));

            // Create Payment entry
            var payment = new Payment
            {
                Amount = totalPrice,
                PaymentDate = DateTime.Now,
                Status = PaymentStatus.Successful,
                PaymentDetails = bookingRequest.PaymentDetails,
            };
            var addedPayment = await _paymentRepository.Add(payment);

            // Create Booking object
            var booking = new Booking
            {
                ScheduleId = bookingRequest.ScheduleId,
                UserId = bookingRequest.UserId,
                BookingTime = DateTime.Now, // Set current booking time
                TotalPrice = totalPrice,
                PaymentId = addedPayment.PaymentId // Assign the PaymentId of the created payment
            };
             

            // Save booking
            var addedBooking = await _bookingRepository.Add(booking);
            // Fetch seat details for selected seats
            var seatDetails = await _seatDetailRepository.GetSeatDetailsAsync(bookingRequest.SelectedSeats);

            if (seatDetails == null || seatDetails.Count() != bookingRequest.SelectedSeats.Count())
            {
                throw new Exception("Invalid seat selection.");
            }


            // Create PassengerBooking entries,SeatNumbers
            int index = 0;
            foreach (var passengerId in bookingRequest.PassengerIds)
            {
                var seatDetail = seatDetails.ElementAtOrDefault(index); // Get the seat detail at the current index
                if (seatDetail != null)
                {
                    var passengerBooking = new PassengerBooking
                    {
                        BookingId = addedBooking.Id,
                        PassengerId = passengerId,
                        SeatNumber = seatDetail.SeatNumber // Assign a unique seat to each passenger
                    };
                    await _passengerBookingRepository.Add(passengerBooking);
                    index++; // Move to the next seat for the next passenger
                }
                else
                {
                    // Handle case where there are not enough seats for all passengers
                    throw new Exception("Not enough seats available for all passengers.");
                }
            }

            return addedBooking != null && addedPayment != null;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _bookingRepository.GetAsync(bookingId);
        }

        public async Task<Booking> CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);
            
            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }
           

            // Remove passenger bookings
            var passengerBookings = await _passengerBookingsRepository.GetPassengerBookingsAsync(bookingId);
            foreach (var passengerBooking in passengerBookings)
            {
                await _passengerBookingRepository.Delete(passengerBooking.Id);
            }

            // Delete payment
            await _paymentRepository.Delete(booking.PaymentId);

            // Delete booking
            return await _bookingRepository.Delete(booking.Id);
        }
        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId)
        {
            return await _bookingsRepository.GetBookingsByUserIdAsync(userId);
        }
        public double CalculateTotalPrice(int numberOfSeats, Flight flight)
        {
            // Calculate total price based on the number of selected seats and any applicable pricing logic
            // For example:
            return numberOfSeats * flight.BasePrice;

        }

        public async Task<bool> RequestRefundAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);

            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }

            // Check if payment exists
            var payment = await _paymentRepository.GetAsync(booking.PaymentId);

            if (payment == null)
            {
                throw new Exception("Payment not found for the booking.");
            }

            // Check payment status
            if (payment.Status != PaymentStatus.Successful)
            {
                throw new Exception("Refund cannot be requested for unsuccessful payments.");
            }

            // Update payment status to "Pending" for refund request
            payment.Status = PaymentStatus.RefundRequested;
            await _paymentRepository.Update(payment);

            // Perform refund process here (e.g., communicate with payment gateway)

            return true;
        }

        public async Task<List<Booking>> GetBookingBySchedule(int scheduleId)
        {
            var bookings = await _bookingRepository.GetAsync();
            bookings = bookings.Where(e => e.ScheduleId == scheduleId).ToList();


            if (bookings != null)
            {
                return bookings;
            }
            throw new NoSuchBookingsException();
        }
    }
}
