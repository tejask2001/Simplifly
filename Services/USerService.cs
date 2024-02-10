
using Simplifly.Mappers;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace Simplifly.Services
{
    public class USerService : IUserService
    {
        
        private readonly IRepository<string, User> _userRepository;
        private readonly ILogger<USerService> _logger;
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly IRepository<int, FlightOwner> _flightownerRepository;
        private readonly ITokenService _tokenService;
        private readonly IRepository<int, Customer> _customerRepository;

        public USerService(IRepository<string, User> userRepository, IRepository<int, Admin> adminRepository,
                           IRepository<int, FlightOwner> flightownerRepository, IRepository<int, Customer> customerRepository,
                            ITokenService tokenService,ILogger<USerService> logger)
        {
            _adminRepository = adminRepository;
            _flightownerRepository = flightownerRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _logger = logger;

        }

        public async Task<LoginUserDTO> Login(LoginUserDTO user)
        {
            var myUSer = await _userRepository.GetAsync(user.Username);
            if (myUSer == null)
            {
                throw new InvlidUuserException();
            }
            var userPassword = GetPasswordEncrypted(user.Password, myUSer.Key);
            var checkPasswordMatch = ComparePasswords(myUSer.Password, userPassword);
            if (checkPasswordMatch)
            {
                user.Password = "";
                user.Role = myUSer.Role;
                user.Token = await _tokenService.GenerateToken(user);
                return user;
            }
            throw new InvlidUuserException();
        }

        private bool ComparePasswords(byte[] password, byte[] userPassword)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] != userPassword[i])
                    return false;
            }
            return true;
        }

        private byte[] GetPasswordEncrypted(string password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            var userpassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userpassword;
        }

        public async Task<LoginUserDTO> RegisterAdmin(RegisterAdminUserDTO user)
        {

            User myuser = new RegisterToUser(user).GetUser();
            myuser = await _userRepository.Add(myuser);
            Admin admin = new RegisterToAdmin(user).GetAdmin();
            admin = await _adminRepository.Add(admin);
            LoginUserDTO result = new LoginUserDTO
            {
                Username = myuser.Username,
                Role = myuser.Role,

            };
            return result;

        }

        public async Task<LoginUserDTO> RegisterFlightOwner(RegisterFlightOwnerUserDTO user)
        {

            User myuser = new RegisterToUser(user).GetUser();
            myuser = await _userRepository.Add(myuser);
            FlightOwner flightowner = new RegisterToFlightOwner(user).GetFlightOwner();
            flightowner = await _flightownerRepository.Add(flightowner);
            LoginUserDTO result = new LoginUserDTO
            {
                Username = myuser.Username,
                Role = myuser.Role,

            };
            return result;

        }public async Task<LoginUserDTO> RegisterCustomer(RegisterCustomerUserDTO user)
        {

            User myuser = new RegisterToUser(user).GetUser();
            myuser = await _userRepository.Add(myuser);
            Customer customer = new RegisterToCustomer(user).GetCustomer();
            customer = await _customerRepository.Add(customer);
            LoginUserDTO result = new LoginUserDTO
            {
                Username = myuser.Username,
                Role = myuser.Role,

            };
            return result;

        }
    }
}