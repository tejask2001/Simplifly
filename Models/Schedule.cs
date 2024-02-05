using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Schedule:IEquatable<Schedule>
    {
        [Key]
        public int Id { get; set; }      
        public string FlightNumber { get; set; }

        //This one is just for navigation and will not be created as an attribute in table
        [ForeignKey("FlightNumber")]
        public Flight? Flight { get; set; }
        public int RouteId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table

        [ForeignKey("RouteId")]   
        public Route? Route { get; set; }

        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }

        public Schedule()
        {
            Id = 0;
        }

        public Schedule(int id, string flightNumber, int routeId, Route? route, DateTime departure, DateTime arrival)
        {
            Id = id;
            FlightNumber = flightNumber;
            RouteId = routeId;
            Route = route;
            Departure = departure;
            Arrival = arrival;
        }
        public Schedule( string flightNumber, int routeId, Route? route, DateTime departure, DateTime arrival)
        {
            
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
