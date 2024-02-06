
using Simplifly.Models.DTOs;

namespace Simplifly.Interfaces
{
    public interface IUserService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> RegisterAdmin(RegisterAdminUserDTO user);

        public Task<LoginUserDTO> RegisterFlightOwner(RegisterFlightOwnerUserDTO user);
        public Task<LoginUserDTO> RegisterCustomer(RegisterCustomerUserDTO user);
    }
}
