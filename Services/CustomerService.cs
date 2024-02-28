using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Models.DTO_s;
using Simplifly.Repositories;
using System.Numerics;

namespace Simplifly.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IRepository<int, Customer> customerRepository, IRepository<string, User> userRepository, ILogger<CustomerService> logger)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _logger = logger;

        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            return await _customerRepository.Add(customer);
        }

        public async Task<bool> RemoveCustomer(int id)
        {
            var cust = await _customerRepository.Delete(id);
            if (cust != null)
            {
                var user = await _userRepository.Delete(cust.Username);
                await _userRepository.Delete(cust.Username);
                return true;
            }
            return false;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAsync();
        }

        public async Task<Customer> UpdateCustomerEmail(int id, string email)
        {
            var cust = await _customerRepository.GetAsync(id);
            if (cust != null)
            {
                cust.Email = email;
                cust = await _customerRepository.Update(cust);
                return cust;
            }
            return null;
        }

        public async Task<Customer> GetByIdCustomers(int id)
        {
            return await (_customerRepository.GetAsync(id));
        }

        public async Task<Customer> GetCustomersByUsername(string username)
        {
            var customers = await _customerRepository.GetAsync();
            var customer=customers.FirstOrDefault(e=>e.Username==username);
            if(customer!= null)
            {
                return customer;
            }
            throw new NoSuchCustomerException();
        }

        public async Task<Customer> UpdateCustomer(UpdateCustomerDTO customer)
        {
            var customers = await _customerRepository.GetAsync(customer.UserId);
            if (customer != null)
            {
                customers.Name=customer.Name;
                customers.Email=customer.Email;
                customers.Phone=customer.Phone;
                customers = await _customerRepository.Update(customers);
                return customers;
            }
            throw new NoSuchCustomerException();
        }
    }
}
