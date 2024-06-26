﻿using Simplifly.Models;
using Simplifly.Models.DTO_s;
using Route = Simplifly.Models.Route;

namespace Simplifly.Interfaces
{
    public interface IFlightFlightOwnerService
    {
        public Task<Flight> AddFlight(Flight flight);
        public Task<Flight> RemoveFlight(string flightNumber);
        public Task<List<Flight>> GetAllFlights();
        public Task<Flight> GetFlightById(string id);
        public Task<Flight> UpdateAirline(string flightNumber, string airline);
        public Task<Flight> UpdateTotalSeats(FlightSeatsDTO flightSeatsDTO);


    }
}