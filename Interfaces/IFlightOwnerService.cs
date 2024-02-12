using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface IFlightOwnerService
    {
        public Task<FlightOwner> AddFlightOwner(FlightOwner flightowner);
        public Task<bool> RemoveFlightOwner(int Id);
        public Task<List<FlightOwner>> GetAllFlightOwners();
        public Task<FlightOwner> GetByIdFlightOwners(int id);
        public Task<FlightOwner> UpdateFlightOwnerAddress(int id ,string address);
    }
}
