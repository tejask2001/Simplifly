using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Schedule:IEquatable<Schedule>
    {
        public int Id { get; set; }
        public string? FlightNumber { get; set; }
        public int RouteId { get; set; }
        [ForeignKey("RouteId")]
        public Route? Route { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }

        public Schedule()
        {
            
        }

        public Schedule(int id, string? flightNumber, int routeId, Route? route, DateTime departure, DateTime arrival)
        {
            Id = id;
            FlightNumber = flightNumber;
            RouteId = routeId;
            Route = route;
            Departure = departure;
            Arrival = arrival;
        }

        public bool Equals(Schedule? other)
        {
            var schedule= other ?? new Schedule();
            return this.Id.Equals(schedule.Id);
        }
    }
}
