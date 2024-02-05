using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;

namespace Simplifly.Repositories
{
    public class ScheduleRepository : IRepository<int, Schedule>
    {
        RequestTrackerContext _context;
        public ScheduleRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<Schedule> Add(Schedule items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        public async Task<Schedule> Delete(Schedule items)
        {
            var schedule = await GetAsync(items.Id);
            if (schedule != null)
            {
                _context.Remove(schedule);
                _context.SaveChanges();
                return items;
            }
            throw new NoSuchScheduleException();
        }

        public async Task<Schedule> GetAsync(int key)
        {
            var schedules = await GetAsync();
            var schedule= schedules.FirstOrDefault(e=>e.Id==key);
            if (schedule != null)
            {
                return schedule;
            }
            throw new NoSuchScheduleException();
        }

        public async Task<List<Schedule>> GetAsync()
        {
            var schedules = _context.Schedules.ToList();
            return schedules;
        }

        public async Task<Schedule> Update(Schedule items)
        {
            var schedule = await GetAsync(items.Id);
            if(schedule != null)
            {
                _context.Entry<Schedule>(items).State=EntityState.Modified;
                _context.SaveChanges();
                return schedule;
            }
            throw new NoSuchScheduleException();
        }
    }
}
