using Microsoft.AspNetCore.Mvc;
using Simplifly.Interfaces;
using System.Threading.Tasks;
using Simplifly.Services;

namespace Simplifly.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class CustomerDashboardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;

        public CustomerDashboardController(IUserService userService, ICustomerService customerService, IBookingService bookingService)
        {
            _userService = userService;
            _bookingService = bookingService;
            _customerService = customerService;
        }

        //[HttpGet("{userId}/bookings")]
        //public async Task<IActionResult> GetBookings(int userId)
        //{
        //    var bookings = await _bookingService.GetUserBookingsAsync(userId);
        //    return Ok(bookings);
        //}

        [HttpPost("{userId}/bookings")]
        public async Task<IActionResult> BookTickets(int userId, [FromBody] BookingRequestDto bookingRequestDto)
        {
            bookingRequestDto.UserId = userId; // Set the user ID in the request DTO
            var bookingId = await _bookingService.CreateBookingAsync(bookingRequestDto);
            return CreatedAtAction(nameof(GetBooking), new { userId, bookingId }, null);
        }

        [HttpGet("{userId}/bookings/{bookingId}")]
        public async Task<IActionResult> GetBooking(int userId, int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking == null || booking.UserId != userId)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpDelete("{userId}/bookings/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int userId, int bookingId)
        {
            var success = await _bookingService.CancelBookingAsync(bookingId);
            if (success != null)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("{userId}/booking-history")]
        public async Task<IActionResult> GetBookingHistory(int userId)
        {
            var bookingHistory = await _bookingService.GetUserBookingsAsync(userId);
            return Ok(bookingHistory);
        }

        [HttpPut("{userId}/bookings/{bookingId}/refund")]
        public async Task<IActionResult> RequestRefund(int userId, int bookingId)
        {
            var success = await _bookingService.RequestRefundAsync(bookingId);
            if (success)
            {
                return Ok("Refund requested successfully.");
            }
            return NotFound();
        }
    }
}
