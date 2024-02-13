using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class PaymentRepository : IRepository<int, Models.Payment>, IPaymentRepository
    {
        RequestTrackerContext _context;

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public PaymentRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add Payment to the database
        /// </summary>
        /// <param name="items">Object of Payment</param>
        /// <returns>Payment object</returns>
        public async Task<Models.Payment> Add(Models.Payment items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        /// <summary>
        /// Method to delete Payment from database
        /// </summary>
        /// <param name="items">Object of Payment</param>
        /// <returns>Payment object</returns>
        /// <exception cref="NoSuchPaymentException">throws exception if no Payment found</exception>
        public async Task<Models.Payment> Delete(int paymentId)
        {
            var payment =  await GetAsync(paymentId);
            if (payment != null)
            {
                _context.Remove(payment);
                _context.SaveChanges();
                return payment;
            }
            throw new NoSuchPaymentException();
        }

        /// <summary>
        /// Method to get Payment data of specific Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Payment Object</returns>
        /// <exception cref="NoSuchPaymentException">throws exception if no Payment found.</exception>
        public async Task<Models.Payment> GetAsync(int key)
        {
            var payments = await GetAsync();
            var payment = payments.FirstOrDefault(e => e.PaymentId == key);
            if (payment != null)
            {
                return payment;
            }
            throw new NoSuchPaymentException();
        }

        /// <summary>
        /// Method to get list of Payment
        /// </summary>
        /// <returns>Payment object</returns>
        public async Task<List<Models.Payment>> GetAsync()
        {
            var payments = _context.Payments.ToList();
            return payments;
        }

        /// <summary>
        /// Method to update Payment.
        /// </summary>
        /// <param name="items">Object of Payment</param>
        /// <returns>Airport Object</returns>
        /// <exception cref="NoSuchPaymentException">throws exception if no Payment found</exception</exception>
        public async Task<Models.Payment> Update(Models.Payment items)
        {
            var payment = await GetAsync(items.PaymentId);
            if (payment != null)
            {
                _context.Entry<Models.Payment>(payment).State = EntityState.Modified;
                _context.SaveChanges();
                return payment;
            }
            throw new NoSuchPaymentException();
        }
        public async Task<Payment> GetPaymentByBookingIdAsync(int bookingId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

    }
}
