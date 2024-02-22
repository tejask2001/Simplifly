using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightOwnerController : ControllerBase
    {
        private readonly IFlightOwnerService _flightOwnerService;
        private readonly ILogger<FlightOwnerController> _logger;

        public FlightOwnerController(IFlightOwnerService flightOwnerService, ILogger<FlightOwnerController> logger)
        {
            _flightOwnerService=flightOwnerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<FlightOwner>> GetFlightOwnerByUsername(string username)
        {
            try
            {
                var flightOwner = await _flightOwnerService.GetByUsernameFlightOwners(username);
                return flightOwner;
            }
            catch(NoSuchFlightOwnerException nsfe)
            {
                _logger.LogInformation(nsfe.Message);
                return NotFound(nsfe.Message);
            }
        }
    }
}
