using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Repositories;
using System.Numerics;

namespace Simplifly.Services
{
    public class PassengerService : IPassengerService
    {
       
        private readonly IRepository<int, Passenger> _passengerRepository;
        private readonly ILogger<PassengerService> _logger;
        public PassengerService(IRepository<int, Passenger> passengerRepository, ILogger<PassengerService> logger)
        {
            _passengerRepository = passengerRepository;
            _logger = logger;

        }

        public async Task<Passenger> AddPassenger(Passenger passenger)
        {
            return await _passengerRepository.Add(passenger);
        }

        public async Task<bool> RemovePassenger(int id)

        {

            var owner = await _passengerRepository.GetAsync(id);
            if (owner != null)
            {
                await _passengerRepository.Delete(id);
                return true;
            }
            return false;
        }

        public async Task<List<Passenger>> GetAllPassengers()
        {
            return await _passengerRepository.GetAsync();
        }

        

        public async Task<Passenger> GetByIdPassengers(int id)
        {
            return await (_passengerRepository.GetAsync(id));
        }

        
    }
}
