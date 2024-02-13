using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Services
{
    public class FlightService : IFlightFlightOwnerService
    {
        private readonly IRepository<string, Flight> _flightRepository;
        private readonly ILogger<FlightService> _logger;
        public FlightService(IRepository<string, Flight> flightRepository, ILogger<FlightService> logger)
        {
            _flightRepository = flightRepository;
            _logger = logger;
        }
        public async Task<Flight> AddFlight(Flight flight)
        {
            try
            {
                var flights = await _flightRepository.GetAsync(flight.FlightNumber);
                throw new FlightAlreadyPresentException();
            }
            catch(NoSuchFlightException)
            {
                flight = await _flightRepository.Add(flight);
                return flight;
            }
            
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            var flights = await _flightRepository.GetAsync();
            return flights;
        }

        public async Task<Flight> GetFlightById(string flightNumber)
        {
            var flight = await _flightRepository.GetAsync(flightNumber);
            if (flight != null)
            {
                return flight;
            }
            throw new NoSuchFlightException();
        }

        public async Task<Flight> RemoveFlight(string flightNumber)
        {
            var flight=await _flightRepository.GetAsync(flightNumber);
            if(flight != null)
            {
                flight = await _flightRepository.Delete(flightNumber);
                return flight;
            }
            throw new NoSuchFlightException();
        }

        public async Task<Flight> UpdateAirline(string flightNumber, string airline)
        {
            var flight= await _flightRepository.GetAsync(flightNumber);
            if(flight != null)
            {
                flight.Airline = airline;
                flight=await _flightRepository.Update(flight);
                return flight;
            }
            throw new NoSuchFlightException();
        }

        public async Task<Flight> UpdateTotalSeats(string flightNumber, int totalSeats)
        {
            var flight = await _flightRepository.GetAsync(flightNumber);
            if(flight!=null)
            {
                flight.TotalSeats = totalSeats;
                flight = await _flightRepository.Update(flight);
                return flight;
            }
            return null;
        }
    }
}
