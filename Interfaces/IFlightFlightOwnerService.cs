using Simplifly.Models;
using Route = Simplifly.Models.Route;

namespace Simplifly.Interfaces
{
    public interface IFlightFlightOwnerService
    {
        public Task<Flight> AddFlight(Flight flight);
        public Task<Flight> RemoveFlight(string flightNumber);
        public Task<List<Flight>> GetAllFlights();
        public Task<List<Flight>> UpdateFlight(Flight flight);
        
    }
}
