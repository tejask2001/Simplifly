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
        private readonly  ISeatDeatilRepository _seatDetailRepository;
        private readonly IPassengerBookingRepository _passengerBookingsRepository;
        private readonly IBookingRepository _bookingsRepository;

        private readonly IRepository<int, Payment> _paymentRepository;
        private readonly IPaymentRepository _paymentsRepository;

        private readonly ILogger<BookingService> _logger;
        public BookingService(IRepository<int, Booking> bookingRepository, IRepository<int, PassengerBooking> passengerBookingRepository, IPaymentRepository paymentsRepository, IRepository<string, Flight> flightRepository, IBookingRepository bookingsRepository, ISeatDeatilRepository seatDetailRepository, IPassengerBookingRepository passengerBookingRepository1, IRepository<int, Payment> paymentRepository, ILogger<BookingService> logger)
        {
            _paymentsRepository = paymentsRepository;
            _flightRepository = flightRepository;
            _bookingsRepository = bookingsRepository;
            _passengerBookingsRepository = passengerBookingRepository1;
            _bookingRepository = bookingRepository;
            _passengerBookingRepository = passengerBookingRepository;
            _seatDetailRepository = seatDetailRepository;
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
            var isSeatsAvailable = await _bookingsRepository.CheckSeatsAvailabilityAsync(bookingRequest.FlightId,bookingRequest.SelectedSeats);

            if (!isSeatsAvailable)
            {
                // Handle case where selected seats are not available
                return false; 
            }
            

            // Calculate total price based on the number of selected seats
            var totalPrice = CalculateTotalPrice(bookingRequest.SelectedSeats.Count, await _flightRepository.GetAsync(bookingRequest.FlightId) );
            // Create Booking object
            var booking = new Booking
            {
                FlightId = bookingRequest.FlightId,
                UserId = bookingRequest.UserId,
                BookingTime = DateTime.Now, // Set current booking time
                TotalPrice = totalPrice,
                Seats = new List<SeatDetail>()
            };

            // Fetch seat details for selected seats
            var seatDetails = await _seatDetailRepository.GetSeatDetailsAsync(bookingRequest.SelectedSeats);

            if (seatDetails == null || seatDetails.Count()!= bookingRequest.SelectedSeats.Count())
            {
                throw new Exception("Invalid seat selection.");
            }

            // Check if selected seats are available
            foreach (var seat in seatDetails)
            {
                if (seat.IsBooked)
                {
                    throw new Exception($"Seat {seat.SeatNumber} is already booked.");
                }
            }

            // Update seat availability and add to booking
            foreach (var seat in seatDetails)
            {
                seat.IsBooked = true;
                booking.Seats.Add(seat);
            }

            // Save booking and seat details
            booking = await _bookingRepository.Add(booking);
            await _seatDetailRepository.UpdateSeatDetailsAsync(seatDetails);

            // Create PassengerBooking entries
            foreach (var passengerId in bookingRequest.PassengerIds)
            {
                var passengerBooking = new PassengerBooking
                {
                    BookingId = booking.Id,
                    PassengerId = passengerId,
                    SeatId = seatDetails.First().Id // Assign the same seat to all passengers for simplicity
                };
                await _passengerBookingRepository.Add(passengerBooking);
            }

            // Create Payment entry
            var payment = new Payment
            {
                Amount = booking.TotalPrice,
                PaymentDate = DateTime.Now,
                Status = PaymentStatus.Pending,
                PaymentDetails = bookingRequest.PaymentDetails,
                BookingId = booking.Id
            };
            await _paymentRepository.Add(payment);

            return booking != null;
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

            // Update seat availability
            foreach (var seat in booking.Seats)
            {
                seat.IsBooked = false;
            }

            // Remove passenger bookings
            var passengerBookings = await _passengerBookingsRepository.GetPassengerBookingsAsync(bookingId);
            foreach (var passengerBooking in passengerBookings)
            {
                await _passengerBookingRepository.Delete(passengerBooking.Id);
            }

            // Delete payment
            await _paymentRepository.Delete(bookingId);

            // Delete booking
            return await _bookingRepository.Delete(booking.Id);
        }
        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId)
        {
            return await _bookingsRepository.GetBookingsByUserIdAsync(userId);
        }
        private double CalculateTotalPrice(int numberOfSeats,Flight flight)
        {
            // Calculate total price based on the number of selected seats and any applicable pricing logic
            // For example:
             return numberOfSeats * flight.BasePrice ;
            
        }

        public async Task<bool> RequestRefundAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);

            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }

            // Check if payment exists
            var payment = await _paymentsRepository.GetPaymentByBookingIdAsync(bookingId);

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
            payment.Status = PaymentStatus.Pending;
            await _paymentRepository.Update(payment);

            // Perform refund process here (e.g., communicate with payment gateway)

            return true;
        }

    }
}
