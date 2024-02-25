using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<int, Admin> _repository;
        private readonly ILogger<AdminService> _logger;
        public AdminService(IRepository<int, Admin> repository, ILogger<AdminService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Admin> GetAdminByUsername(string username)
        {
            var admins = await _repository.GetAsync();
            var admin= admins.FirstOrDefault(e=>e.Username==username);
            if (admin!=null) return admin;
            throw new NoSuchAdminException();
        }
    }
}
