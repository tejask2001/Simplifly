using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class CustomerRepository : IRepository<int, Customer>
    {
        private readonly RequestTrackerContext _context;
        private readonly ILogger<CustomerRepository> _logger;
        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public CustomerRepository(RequestTrackerContext context,ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Customer> Add(Customer items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        public async Task<Customer> Delete(int ownerId)
        {
            var customer = await GetAsync(ownerId);
            if (customer != null)
            {
                _context?.Remove(customer);
                _context.SaveChanges();
                return customer;
            }
            throw new NoSuchCustomerException();
        }

        public async Task<Customer> GetAsync(int key)
        {
            var customers = await GetAsync();
            var customer = customers.FirstOrDefault(e => e.UserId == key);
            if (customer != null)
            {
                return customer;
            }
            throw new NoSuchCustomerException();
        }

        public async Task<List<Customer>> GetAsync()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }

        public async Task<Customer> Update(Customer items)
        {
            var customer = await GetAsync(items.UserId);
            if (customer != null)
            {
                _context.Entry<Customer>(items).State = EntityState.Modified;
                _context.SaveChanges();
                return customer;
            }
            throw new NoSuchCustomerException();
        }
    }
}
