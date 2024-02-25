using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface IAdminService
    {
        public Task<Admin> GetAdminByUsername(string username);
    }
}
