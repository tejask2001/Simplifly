using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Models.DTO_s;

namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightFlightOwnerService _flightOwnerService;
        public FlightController(IFlightFlightOwnerService flightOwnerService)
        {
            _flightOwnerService = flightOwnerService;
        }


        [HttpGet]
        [Authorize(Roles ="Admin")]
        public Task<List<Flight>> GetAllFlight()
        {
            var flights=_flightOwnerService.GetAllFlights();
            return flights;
        }

        [HttpPost]
        [Authorize(Roles = "flightOwner")]
        public async Task<Flight> AddFlight(Flight flight)
        {
            flight= await _flightOwnerService.AddFlight(flight);
            return flight;
        }
                

        [HttpPut]
        [Authorize(Roles = "flightOwner")]
        public async Task<Flight> UpdateFlightAirline(FlightAirlineDTO flightDTO)
        {
            var flight= await _flightOwnerService.UpdateAirline(flightDTO.FlightNumber, flightDTO.Airline);
            return flight;
        }

        [Route("UpdateTotalSeats")]
        [HttpPut]
        [Authorize(Roles = "flightOwner")]
        public async Task<Flight> UpdateTotalSeats(FlightSeatsDTO flightDTO)
        {
            var flight = await _flightOwnerService.UpdateTotalSeats(flightDTO.FlightNumber, flightDTO.TotalSeats);
            return flight;
        }

        [HttpDelete]
        [Authorize(Roles = "flightOwner")]
        public async Task<Flight> RemoveFlight(string flightNumber)
        {
            var flight = await _flightOwnerService.RemoveFlight(flightNumber);
            return flight;
        }

    }
}
