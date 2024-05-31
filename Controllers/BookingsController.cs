using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using System.Diagnostics.CodeAnalysis;

namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ICancelledBookingService _cancelledBookingService;
        private readonly ILogger<BookingsController> _logger;
        public BookingsController(IBookingService bookingService, ICancelledBookingService cancelledBookingService, ILogger<BookingsController> logger)
        {
            _bookingService = bookingService;
            _cancelledBookingService = cancelledBookingService;
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

        [Route("GetBookedSeats")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetBookedSeats(int scheduleId)
        {
            try
            {
                var bookedSeats=await _bookingService.GetBookedSeatBySchedule(scheduleId);
                return bookedSeats;
            }
            catch(NoSuchBookingsException nsbe)
            {
                _logger.LogInformation(nsbe.Message);
                return NotFound(nsbe.Message);
            }
        }

        [Route("GetCancelledBookingByUserId")]
        [HttpGet]
        public async Task<ActionResult<List<CancelledBooking>>> GetCancelledBooking()
        {
            try
            {
                var cancelledBooking = await _cancelledBookingService.GetCancelledBooking();
                return Ok(cancelledBooking);
            }
            catch (NoCancelledBookingFound ncbf)
            {
                _logger.LogInformation(ncbf.Message);
                return NotFound(ncbf.Message);
            }
        }

        [Route("GetRefundRequest")]
        [HttpGet]
        public async Task<ActionResult<List<CancelledBooking>>> GetRefundRequest()
        {
            try
            {
                var cancelledBooking = await _cancelledBookingService.GetRefundRequest();
                return Ok(cancelledBooking);
            }
            catch (NoCancelledBookingFound ncbf)
            {
                _logger.LogInformation(ncbf.Message);
                return NotFound(ncbf.Message);
            }
        }

        [Route("CancelBooking")]
        [HttpDelete]
        public async Task<ActionResult<CancelledBooking>> CancelBooking(int bookingId)
        {
            try
            {
                var cancelledBooking = await _cancelledBookingService.AddCancelledBooking(bookingId);
                return cancelledBooking;
            }
            catch(NoCancelledBookingFound ncbe)
            {
                _logger.LogInformation(ncbe.Message);
                return NotFound(ncbe.Message);
            }
        }

        [Route("ChangeRefundStatus")]
        [HttpPut]
        public async Task<ActionResult<CancelledBooking>> ChangeRefundStatus(int id)
        {
            try
            {
                var cancelBooking = await _cancelledBookingService.UpdateCancelledBooking(id);
                return Ok(cancelBooking);
            }
            catch (NoCancelledBookingFound ncbe)
            {
                _logger.LogInformation(ncbe.Message);
                return NotFound(ncbe.Message);
            }
        }
    }
}
