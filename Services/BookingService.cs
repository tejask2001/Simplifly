using Microsoft.AspNetCore.Mvc;
using Simplifly.Controllers;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Models.DTO_s;
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

        /// <summary>
        /// Constructor to initialize objects
        /// </summary>
        /// <param name="bookingRepository"></param>
        /// <param name="scheduleRepository"></param>
        /// <param name="passengerBookingRepository"></param>
        /// <param name="flightRepository"></param>
        /// <param name="bookingsRepository"></param>
        /// <param name="seatDetailRepository"></param>
        /// <param name="passengerBookingRepository1"></param>
        /// <param name="paymentRepository"></param>
        /// <param name="logger"></param>
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

        /// <summary>
        /// Method to Create Booking
        /// </summary>
        /// <param name="bookingRequest">Object ob BookingRequestDTO</param>
        /// <returns>bool based on booking status</returns>
        /// <exception cref="ArgumentNullException">If null value encountered</exception>
        /// <exception cref="Exception">If schedule not found or invalid seat selected</exception>
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
            var seatClass = bookingRequest.SelectedSeats[0][0];
            if (seatClass == 'E')
            {
                totalPrice += 800;
            }

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

        /// <summary>
        /// Method to get all bookings
        /// </summary>
        /// <returns>Collection of Booking</returns>
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAsync();
        }

        /// <summary>
        /// Method to get booking by Id
        /// </summary>
        /// <param name="bookingId">Id in int</param>
        /// <returns>Object of booking</returns>
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _bookingRepository.GetAsync(bookingId);
        }

        /// <summary>
        /// Method to get booking by Id
        /// </summary>
        /// <param name="bookingId">Id in in</param>
        /// <returns>Object of booking</returns>
        /// <exception cref="NoSuchBookingsException">throws when no booking found</exception>
        public async Task<Booking> CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);
            
            if (booking == null)
            {
                throw new NoSuchBookingsException();
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

        /// <summary>
        /// Method to get booking by userID
        /// </summary>
        /// <param name="userId">UserId in int</param>
        /// <returns>Collection og Booking</returns>
        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId)
        {
            var bookings= await _bookingsRepository.GetBookingsByUserIdAsync(userId);
            if (bookings != null)
            {
                bookings.Where(e => e.Schedule.Departure > DateTime.Now);
            }
            return await _bookingsRepository.GetBookingsByUserIdAsync(userId);
        }

        /// <summary>
        /// Method to calculate total price of booking
        /// </summary>
        /// <param name="numberOfSeats">Total seats in int</param>
        /// <param name="flight">Object of flight</param>
        /// <returns></returns>
        public double CalculateTotalPrice(int numberOfSeats, Flight flight)
        {
            double totalPrice = numberOfSeats * (flight?.BasePrice ?? 0); 
            return totalPrice;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchBookingsException">When booking with given id not found</exception>
        public async Task<bool> RequestRefundAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);

            if (booking == null)
            {
                throw new NoSuchBookingsException();
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

        /// <summary>
        /// Method to get booking by scheduleId
        /// </summary>
        /// <param name="scheduleId">ScheduleId in int</param>
        /// <returns>List of Booking</returns>
        /// <exception cref="NoSuchBookingsException">when booking with given scheduleId not found</exception>
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

        /// <summary>
        /// Method to get booking by flight Number
        /// </summary>
        /// <param name="flightNumber">Flight number in string</param>
        /// <returns>List of booking</returns>
        /// <exception cref="NoSuchBookingsException">When booking with flight number not found</exception>
        public async Task<List<Booking>> GetBookingByFlight(string flightNumber)
        {
            var bookings = await _bookingRepository.GetAsync();
            bookings = bookings.Where(e => e.Schedule.FlightId == flightNumber).ToList();
            if (bookings != null)
            {
                return bookings;
            }
            throw new NoSuchBookingsException();
        }

        /// <summary>
        /// Method to get booked seat for particular schedule
        /// </summary>
        /// <param name="scheduleID">scheduleId in int</param>
        /// <returns>List of booked seat in string</returns>
        /// <exception cref="NoSuchBookingsException">when no seats found with given schedule</exception>
        public async Task<List<string>> GetBookedSeatBySchedule(int scheduleID)
        {
            var bookings=await _passengerBookingRepository.GetAsync();
            var bookedSeats= bookings.Where(e=>e.Booking.ScheduleId==scheduleID)
                .Select(e=>e.SeatNumber).ToList();
            if(bookedSeats != null)
            {
                return bookedSeats;
            }
            throw new NoSuchBookingsException();
        }

        public async Task<List<PassengerBooking>> GetBookingsByCustomerId(int customerId)
        {
            var bookings = await _passengerBookingRepository.GetAsync();
            bookings = bookings.Where(e => e.Booking.UserId == customerId).ToList();
            if(bookings!= null)
            {
                return bookings;
            }
            throw new NoSuchCustomerException();
        }
    }
}
