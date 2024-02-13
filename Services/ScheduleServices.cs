using Simplifly.Exceptions;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Models.DTO_s;
using Simplifly.Repositories;

namespace Simplifly.Services
{
    public class ScheduleServices : IScheduleFlightOwnerService, IFlightCustomerService
    {
        IRepository<int, Schedule> _scheduleRepository;

        ILogger<ScheduleServices> _logger;


        /// <summary>
        /// Constructor to initialize the objects
        /// </summary>
        /// <param name="scheduleRepository"></param>
        /// <param name="logger"></param>
        public ScheduleServices(IRepository<int, Schedule> scheduleRepository, ILogger<ScheduleServices> logger)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;

        }

        /// <summary>
        ///Service class method to add schedule 
        /// </summary>
        /// <param name="schedule">Object of schedule</param>
        /// <returns>Schedule object</returns>
        /// <exception cref="FlightScheduleBusyException">Throw when schedule is already present</exception>
        public async Task<Schedule> AddSchedule(Schedule schedule)
        {
            var existingSchedules = await _scheduleRepository.GetAsync();
            bool isOverlap = existingSchedules.Any(e =>
    e.FlightId == schedule.FlightId &&
    ((schedule.Departure >= e.Departure && schedule.Departure <= e.Arrival) ||
    (schedule.Arrival >= e.Departure && schedule.Arrival <= e.Arrival) ||
    (e.Departure >= schedule.Departure && e.Arrival <= schedule.Arrival)));

            if (!isOverlap)
            {
                // If no overlap, add the new schedule
                schedule = await _scheduleRepository.Add(schedule);
                return schedule;
            }
            throw new FlightScheduleBusyException();

        }

        /// <summary>
        /// Service class method to get all schedule
        /// </summary>
        /// <returns>List of schedules</returns>
        public async Task<List<Schedule>> GetAllSchedules()
        {
            var schedules = await _scheduleRepository.GetAsync();
            return schedules;
        }

        /// <summary>
        /// Service class method to remove schedule
        /// </summary>
        /// <param name="flightNumber">Schedule number in string</param>
        /// <returns>Object of schedule</returns>
        /// <exception cref="NoSuchScheduleException">throw when schedule is not present</exception>
        public async Task<Schedule> RemoveSchedule(int scheduleId)
        {
            var schedules = await _scheduleRepository.GetAsync(scheduleId);
            if (schedules != null)
            {
                schedules = await _scheduleRepository.Delete(schedules.Id);
                return schedules;
            }
            throw new NoSuchScheduleException();
        }


        /// <summary>
        /// Service class method to update flight schedule
        /// </summary>
        /// <param name="scheduleId">scheduleId in int</param>
        /// <param name="flightNumber">flightNumber in string</param>
        /// <returns>Object of schedule</returns>
        /// <exception cref="NoSuchScheduleException">throw when no schedule is found</exception>
        public async Task<Schedule> UpdateScheduledFlight(int scheduleId, string flightNumber)
        {
            var schedule = await _scheduleRepository.GetAsync(scheduleId);
            if (schedule != null)
            {
                schedule.FlightId = flightNumber;

                schedule = await _scheduleRepository.Update(schedule);
                return schedule;
            }
            throw new NoSuchScheduleException();
        }

        /// <summary>
        /// Service class method to update scheduled route
        /// </summary>
        /// <param name="scheduleId">scheduleId in int</param>
        /// <param name="routeId">routeId in string</param>
        /// <returns>Object of schedule</returns>
        /// <exception cref="NoSuchScheduleException">throw when no schedule is found</exception>
        public async Task<Schedule> UpdateScheduledRoute(int scheduleId, int routeId)
        {
            var schedule = await _scheduleRepository.GetAsync(scheduleId);
            if (schedule != null)
            {
                schedule.RouteId = routeId;
                schedule = await _scheduleRepository.Update(schedule);
                return schedule;
            }
            throw new NoSuchScheduleException();
        }

        /// <summary>
        /// Service class method to update scheduled route
        /// </summary>
        /// <param name="scheduleId">scheduleId in int</param>
        /// <param name="departure">departure in Datetime</param>
        /// <param name="arrival">arrival in Datetime</param>
        /// <returns>Object of schedule</returns>
        /// <exception cref="NoSuchScheduleException">throw when no schedule is found</exception>
        public async Task<Schedule> UpdateScheduledTime(int scheduleId, DateTime departure, DateTime arrival)
        {
            var schedule = await _scheduleRepository.GetAsync(scheduleId);
            if (schedule != null)
            {
                schedule.Departure = departure;
                schedule.Arrival = arrival;
                schedule = await _scheduleRepository.Update(schedule);
                return schedule;
            }
            throw new NoSuchScheduleException();
        }

        /// <summary>
        /// Method to search flights based on origin, destination and date of travel.
        /// </summary>
        /// <param name="searchFlight">Object of SearchFlightDTO</param>
        /// <returns>List of flight based on user search</returns>
        /// <exception cref="NoFlightAvailableException">Throw when no flight available for particular search.</exception>
        public async Task<List<SearchedFlightResultDTO>> SearchFlights(SearchFlightDTO searchFlight)
        {
            List<SearchedFlightResultDTO> searchResult = new List<SearchedFlightResultDTO>();
            var schedules = await _scheduleRepository.GetAsync();
            schedules = schedules.Where(e => e.Departure.Date == searchFlight.DateOfJourney.Date
             && e.Route?.SourceAirport?.City == searchFlight.Origin
             && e.Route.DestinationAirport?.City == searchFlight.Destination).ToList();

            searchResult = schedules.Select(e => new SearchedFlightResultDTO
            {
                FlightNumber = e.FlightId,
                Airline = e.Flight.Airline,
                SourceAirport = e.Route?.SourceAirport?.Name + " ," + e.Route?.SourceAirport?.City,
                DestinationAirport = e.Route?.DestinationAirport?.Name + " ," + e.Route?.DestinationAirport?.City,
                DepartureTime = e.Departure,
                ArrivalTime = e.Arrival

            }).ToList();
            if (searchResult != null)
                return searchResult;
            else
                throw new NoFlightAvailableException();
        }

        public async Task<List<FlightScheduleDTO>> GetFlightSchedules(string flightNumber)
        {
            List<FlightScheduleDTO> flightSchedule = new List<FlightScheduleDTO>();

            var schedules = await _scheduleRepository.GetAsync();
            schedules = schedules.Where(e => e.FlightId == flightNumber).ToList();

            flightSchedule = schedules.Select(e => new FlightScheduleDTO
            {
                FlightNumber = e.FlightId,

                SourceAirport = e.Route?.SourceAirport?.Name + " ," + e.Route?.SourceAirport?.City,
                DestinationAirport = e.Route?.DestinationAirport?.Name + " ," + e.Route?.DestinationAirport?.City,
                Departure = e.Departure,
                Arrival = e.Arrival
            }).ToList();

            if (flightSchedule != null)
                return flightSchedule;
            else
                throw new NoSuchScheduleException();
        }
    }
}