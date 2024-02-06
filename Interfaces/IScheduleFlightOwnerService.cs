using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface IScheduleFlightOwnerService
    {
        public Task<Schedule> AddSchedule(Schedule schedule);
        public Task<Schedule> RemoveSchedule(Schedule schedule);
        public Task<List<Schedule>> GetAllSchedules();
        public Task<List<Schedule>> UpdateSchedule(Schedule schedule);
    }
}
