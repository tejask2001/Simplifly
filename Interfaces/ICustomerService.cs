using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface ICustomerService
    {
        public Task<Customer> AddCustomer(Customer customer);
        public Task<bool> RemoveCustomer(int Id);
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer> GetByIdCustomers(int id);
        public Task<Customer> GetCustomersByUsername(string username);
        public Task<Customer> UpdateCustomerEmail(int id, string Email);
    }
}
