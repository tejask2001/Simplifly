using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class AdminRepository : IRepository<int, Admin>
    {
        readonly RequestTrackerContext _context;

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public AdminRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<Admin> Add(Admin items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        public Task<Admin> Delete(int ownerId)
        {
            var admin = GetAsync(ownerId);
            if (admin != null)
            {
                _context.Remove(admin);
                _context.SaveChanges();
                return admin;
            }
            throw new NoSuchAdminException();
        }

        public async Task<Admin> GetAsync(int key)
        {
            var admins = await GetAsync();
            var admin = admins.FirstOrDefault(e => e.AdminId == key);
            if (admin != null)
            {
                return admin;
            }
            throw new NoSuchAdminException();
        }

        public async Task<List<Admin>> GetAsync()
        {
            var admins = _context.Admins.ToList();
            return admins;
        }

        public async Task<Admin> Update(Admin items)
        {
            var admin = await GetAsync(items.AdminId);
            if (admin != null)
            {
                _context.Entry<Admin>(items).State = EntityState.Modified;
                _context.SaveChanges();
                return admin;
            }
            throw new NoSuchAdminException();
        }
    }
}
