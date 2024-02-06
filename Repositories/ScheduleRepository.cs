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

        /// <summary>
        /// Default constructor with RequestTrackerContext
        /// </summary>
        /// <param name="context">Database context</param>
        public ScheduleRepository(RequestTrackerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to add Schedule to the database
        /// </summary>
        /// <param name="items">Object of Schedule</param>
        /// <returns>Schedule object</returns>
        public async Task<Schedule> Add(Schedule items)
        {
            _context.Add(items);
            _context.SaveChanges();
            return items;
        }

        /// <summary>
        /// Method to delete Schedule from database
        /// </summary>
        /// <param name="items">Object of Schedule</param>
        /// <returns>Schedule object</returns>
        /// <exception cref="NoSuchScheduleException">throws exception if no Schedule found</exception>
        public async Task<Schedule> Delete(int scheduleId)
        {
            var schedule = await GetAsync(scheduleId);
            if (schedule != null)
            {
                _context.Remove(schedule);
                _context.SaveChanges();
                return schedule;
            }
            throw new NoSuchScheduleException();
        }

        /// <summary>
        /// Method to get Schedule data of specific Id
        /// </summary>
        /// <param name="key">key in int</param>
        /// <returns>Schedule Object</returns>
        /// <exception cref="NoSuchScheduleException">throws exception if no Schedule found.</exception>
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

        /// <summary>
        /// Method to get list of Schedule
        /// </summary>
        /// <returns>Schedule object</returns>
        public async Task<List<Schedule>> GetAsync()
        {
            var schedules = _context.Schedules.ToList();
            return schedules;
        }

        /// <summary>
        /// Method to update Schedule.
        /// </summary>
        /// <param name="items">Object of Schedule</param>
        /// <returns>Schedule Object</returns>
        /// <exception cref="NoSuchScheduleException">throws exception if no Schedule found</exception</exception>

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
