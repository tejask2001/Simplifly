using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Flight:IEquatable<Flight>
    {
        [Key]
        public string FlightNumber { get; set; } 
        public string Airline { get; set; }= string.Empty;
        public int TotalSeats { get; set; }
        public int FlightOwnerOwnerId { get; set; }
        [ForeignKey("FlightOwnerOwnerId")]
        public FlightOwner? FlightOwner { get; set; }

        [ForeignKey("FlightOwnerId")]
        public int FlightOwner {  get; set; }


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
