using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Services
{
    public class FlightService : IFlightFlightOwnerService
    {
        private readonly IRepository<string, Flight> _flightRepository;
        public FlightService(IRepository<string, Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public async Task<Flight> AddFlight(Flight flight)
        {
            flight=await _flightRepository.Add(flight);
            return flight;
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            var flights = await _flightRepository.GetAsync();
            return flights;
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

        public Task<List<Flight>> UpdateFlight(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
