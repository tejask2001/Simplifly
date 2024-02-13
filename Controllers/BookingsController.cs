using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingsController> _logger;
        public BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [Authorize(Roles ="flight owner")]
        [Route("GetBookingByFlight")]
        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetBookingByFlight(string flightNumber)
        {
            try
            {
                var bookings = await _bookingService.GetBookingByFlight(flightNumber);
                return bookings;
            }
            catch(NoSuchBookingsException nsbe)
            {
                _logger.LogInformation(nsbe.Message);
                return NotFound(nsbe.Message);
            }
        }
    }
}
