using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Simplifly.Models;
using Simplifly.Services;
using Simplifly.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Simplifly.Controllers
{
    [Route("api/admin/dashboard")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly ICustomerService _custService;
        private readonly IFlightOwnerService _flightOwnerService;
        private readonly IFlightFlightOwnerService _flightService;
        private readonly IBookingService _bookingService;
        private readonly IRouteFlightOwnerService _routeService;

        public AdminDashboardController(ICustomerService custService, IFlightOwnerService flightownerService, IFlightFlightOwnerService flightService, IBookingService bookingService, IRouteFlightOwnerService routeService)
        {
            _custService = custService;
            _flightOwnerService = flightownerService;
            _flightService = flightService;
            _bookingService = bookingService;
            _routeService =routeService;
        }

        // DELETE: api/admin/dashboard/users/{userId}
        [HttpDelete("customers/{userId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCustomer(int userId)
        {
            var result = await _custService.RemoveCustomer(userId);
            if (result)
            {
                return Ok("User deleted successfully.");
            }
            return NotFound("User not found.");
        }

        // DELETE: api/admin/dashboard/flightowners/{flightOwnerId}
        [HttpDelete("flightowners/{flightOwnerId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteFlightOwner(int flightOwnerId)
        {
            var result = await _flightOwnerService.RemoveFlightOwner(flightOwnerId);
            if (result)
            {
                return Ok("User deleted successfully.");
            }
            return NotFound("User not found.");


        }

        // DELETE: api/admin/dashboard/bookings/{bookingId}
        [HttpDelete("bookings/{bookingId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CancelBooking( int bookingId)
        {
            var success = await _bookingService.CancelBookingAsync(bookingId);
            if (success != null)
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/admin/dashboard/flightroutes/{flightRouteId}
        [HttpDelete("flightroutes/{flightRouteId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteFlightRoute(int flightRouteId)
        {
            var result = await _routeService.RemoveRouteById(flightRouteId);
            if (result)
            {
                return Ok("Flight route deleted successfully.");
            }
            return NotFound("Flight route not found.");
        }
    }
}
