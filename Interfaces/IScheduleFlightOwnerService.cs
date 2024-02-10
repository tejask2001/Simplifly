using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface IScheduleFlightOwnerService
    {
        public Task<Schedule> AddSchedule(Schedule schedule);
        public Task<Schedule> RemoveSchedule(Schedule schedule);
        public Task<List<Schedule>> GetAllSchedules();
        public Task<Schedule> UpdateScheduledFlight(int scheduleId,string flightNumber);
        public Task<Schedule> UpdateScheduledRoute(int scheduleId, int routeId);
        public Task<Schedule> UpdateScheduledTime(int scheduleId, DateTime departure, DateTime arrival);
    }
}