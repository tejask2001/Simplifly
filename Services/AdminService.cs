using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Models.DTO_s;

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

        public async Task<Admin> UpdateAdmin(UpdateAdminDTO admin)
        {
            var admins = await _repository.GetAsync(admin.AdminId);
            if (admins != null)
            {
                admins.Name = admin.Name;
                admins.Email = admin.Email;
                admins.Address = admin.Address;
                admins.ContactNumber = admin.ContactNumber;
                admins.Position = admin.Position;
                admins = await _repository.Update(admins);
                return admins;
            }
            throw new NoSuchAdminException();
        }
    }
}
