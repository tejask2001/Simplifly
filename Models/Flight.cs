﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Flight:IEquatable<Flight>
    {
        [Key]
        public string FlightNumber { get; set; } 
        public string Airline { get; set; }= string.Empty;
        public int TotalSeats { get; set; }

        [ForeignKey("FlightOwnerOwnerId")]
        public int FlightOwnerOwnerId { get; set; }
       
        public FlightOwner? FlightOwner { get; set; }

        public double BasePrice { get; set; }


        public Flight()
        {
           FlightNumber = string.Empty;
            
        }

        public Flight(string flightNumber, string airline, int totalSeats, int flightOwnerOwnerId, double basePrice)
        {
            FlightNumber = flightNumber;
            Airline = airline;
            TotalSeats = totalSeats;
            FlightOwnerOwnerId = flightOwnerOwnerId;
            BasePrice = basePrice;
        }

        public bool Equals(Flight? other)
        {
            var flight = other ?? new Flight();
            return this.FlightNumber.Equals(flight.FlightNumber);
        }
    }
}
