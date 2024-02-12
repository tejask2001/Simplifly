using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Repositories;

namespace Simplifly.Services
{
    public class ScheduleServices : IScheduleFlightOwnerService
    {
        IRepository<int,Schedule> _scheduleRepository;
        ILogger<ScheduleServices> _logger;  

        public ScheduleServices(IRepository<int, Schedule> scheduleRepository, ILogger<ScheduleServices> logger)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;

        }
        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            
            try
            {
                var existingSchedules = await _scheduleRepository.GetAsync(schedule.Id);
                throw new FlightScheduleBusyException();
            }
            catch (NoSuchScheduleException)
            {
                schedule = await _scheduleRepository.Add(schedule);
                return schedule;
            }
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            var schedules = await _scheduleRepository.GetAsync();
            return schedules;
        }

        public Task<Schedule> RemoveSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public async Task<Schedule> UpdateScheduledFlight(int scheduleId, string flightNumber)
        {
            var schedule = await _scheduleRepository.GetAsync(scheduleId);
            if (schedule != null)
            {
                schedule = await _scheduleRepository.Update(schedule);
                return schedule;
            }
            throw new NoSuchScheduleException();
        }

        public async Task<Schedule> UpdateScheduledRoute(int scheduleId, int routeId)
        {
            var schedule = await _scheduleRepository.GetAsync(scheduleId);
            if (schedule != null)
            {
                schedule.RouteId = routeId;
                schedule=await _scheduleRepository.Update(schedule);
                return schedule;
            }
            throw new NoSuchScheduleException();
        }

        public async Task<Schedule> UpdateScheduledTime(int scheduleId, DateTime departure, DateTime arrival)
        {
            var schedule = await _scheduleRepository.GetAsync(scheduleId);
            if (schedule != null)
            {
                schedule.Departure= departure;
                schedule.Arrival= arrival;
                schedule = await _scheduleRepository.Update(schedule);
                return schedule;
            }
            throw new NoSuchScheduleException();
        }
    }
}
