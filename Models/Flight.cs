﻿using System.ComponentModel.DataAnnotations;

namespace Simplifly.Models
{
    public class Flight:IEquatable<Flight>
    {
        [Key]
        public string FlightNumber { get; set; } 
        public string Airline { get; set; }= string.Empty;
        public int TotalSeats { get; set; }

        public Flight()
        {
           FlightNumber = string.Empty;
            
        }

        public Flight(string flightNumber, string airline, int totalSeats)
        {
            FlightNumber = flightNumber;
            Airline = airline;
            TotalSeats = totalSeats;
        }

        public bool Equals(Flight? other)
        {
            var flight = other ?? new Flight();
            return this.FlightNumber.Equals(flight.FlightNumber);
        }
    }
}
