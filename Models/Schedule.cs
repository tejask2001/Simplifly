﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Schedule:IEquatable<Schedule>
    {
        [Key]
        public int Id { get; set; }      
      
        public int RouteId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table

        [ForeignKey("RouteId")]   
        public Route? Route { get; set; }

        [ForeignKey("FlightId")]
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }

        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }


        public Schedule()
        {
            Id = 0;
        }

        public Schedule(int id, int routeId, Route? route, int flightId, DateTime departure, DateTime arrival)
        {
            Id = id;
            RouteId = routeId;
            Route = route;
            Departure = departure;
            Arrival = arrival;
            FlightId = flightId;
        }
        public Schedule(  int routeId, Route? route, int flightId, DateTime departure, DateTime arrival)
        {

            RouteId = routeId;
            Route = route;
            Departure = departure;
            Arrival = arrival;
            FlightId = flightId; 
        }

        public bool Equals(Schedule? other)
        {
            var schedule= other ?? new Schedule();
            return this.Id.Equals(schedule.Id);
        }
    }
}
