using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Models.DTO_s;

namespace Simplifly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleFlightOwnerService _scheduleFlightOwnerService;
        public ScheduleController(IScheduleFlightOwnerService scheduleFlightOwnerService)
        {
            _scheduleFlightOwnerService = scheduleFlightOwnerService;
        }

        [HttpGet]
        public async Task<List<Schedule>> GetAllSchedule()
        {
            var schedules = await _scheduleFlightOwnerService.GetAllSchedules();
            return schedules;
        }

        [HttpPost]
        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            schedule = await _scheduleFlightOwnerService.AddSchedule(schedule);
            return schedule;
        }

        [Route("UpdateScheduledFlight")]
        [HttpPut]
        public async Task<Schedule> UpdateScheduledFlight(ScheduleFlightDTO scheduleFlightDTO)
        {
            var schedule = await _scheduleFlightOwnerService.
                UpdateScheduledFlight(scheduleFlightDTO.ScheduleId, scheduleFlightDTO.FlightNumber);
            return schedule;
        }

        [Route("UpdateScheduledRoute")]
        [HttpPut]
        public async Task<Schedule> UpdateScheduledRoute(ScheduleRouteDTO scheduleRouteDTO)
        {
            var schedule = await _scheduleFlightOwnerService.
                UpdateScheduledRoute(scheduleRouteDTO.ScheduleId, scheduleRouteDTO.RouteId);
            return schedule;
        }

        [Route("UpdateScheduledTime")]
        [HttpPut]
        public async Task<Schedule> UpdateScheduledTime(ScheduleTimeDTO scheduleTimeDTO)
        {
            var schedule = await _scheduleFlightOwnerService.
                UpdateScheduledTime(scheduleTimeDTO.ScheduleId, scheduleTimeDTO.DepartureTime, 
                scheduleTimeDTO.ArrivalTime);
            return schedule;
        }
    }
}
