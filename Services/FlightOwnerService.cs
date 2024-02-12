using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Repositories;
using System.Numerics;

namespace Simplifly.Services
{
    public class FlightOwnerService: IFlightOwnerService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, FlightOwner> _flightownerRepository;
        private readonly ILogger<FlightOwnerService> _logger;
        public FlightOwnerService(IRepository<int, FlightOwner> flightownerRepository,  IRepository<string, User> userRepository, ILogger<FlightOwnerService> logger)
        {
            _userRepository = userRepository;
            _flightownerRepository = flightownerRepository;
            _logger = logger;

        }

        public async Task<FlightOwner> AddFlightOwner(FlightOwner flightOwner)
        {
            return await _flightownerRepository.Add(flightOwner);
        }

        public async Task<bool> RemoveFlightOwner(int id)

        {
            
            var owner =  await _flightownerRepository.GetAsync(id);
            if(owner != null)
            {
                await _flightownerRepository.Delete(id);
               await _userRepository.Delete(owner.Username);
                return true;
            }
            return false;
        }

        public async Task<List<FlightOwner>> GetAllFlightOwners()
        {
            return await _flightownerRepository.GetAsync();
        }

        public async Task<FlightOwner> UpdateFlightOwnerAddress(int id,string address)
        {
            var owner = await _flightownerRepository.GetAsync(id);
            if (owner != null)
            {
                owner.Address = address;
                owner = await _flightownerRepository.Update(owner);
                return owner;
            }
            return null;
        }

        public async Task<FlightOwner> GetByIdFlightOwners(int id)
        {
            return await (_flightownerRepository.GetAsync(id));
        }
    }
}
