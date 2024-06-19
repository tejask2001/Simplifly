using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Simplifly.Models
{
    [ExcludeFromCodeCoverage]
    public class Flight:IEquatable<Flight>
    {
        [Key]
        public string FlightNumber { get; set; } 
        public string Airline { get; set; }= string.Empty;
        public int TotalEconomySeats { get; set; }
        public int TotalPremiumEconomySeats { get; set; }
        public int TotalBusinessClassSeats { get; set; }
        public double EconomySeatPrice { get; set; }
        public double PremiumEconomySeatPrice { get; set; }
        public double BusinessClassSeatPrice { get; set; }


        [ForeignKey("FlightOwnerOwnerId")]
        public int FlightOwnerOwnerId { get; set; }
       
        public FlightOwner? FlightOwner { get; set; }

        public int Status {  get; set; }


        public Flight()
        {
           FlightNumber = string.Empty;
            
        }

        public Flight(string flightNumber, string airline, int totalEconomySeats, int totalPremiumEconomySeats, int totalBusinessClassSeats, double economySeatPrice, double premiumEconomySeatPrice, double lBusinessClassSeatPrice, int flightOwnerOwnerId, FlightOwner? flightOwner, int status)
        {
            FlightNumber = flightNumber;
            Airline = airline;
            TotalEconomySeats = totalEconomySeats;
            TotalPremiumEconomySeats = totalPremiumEconomySeats;
            TotalBusinessClassSeats = totalBusinessClassSeats;
            EconomySeatPrice = economySeatPrice;
            PremiumEconomySeatPrice = premiumEconomySeatPrice;
            this.BusinessClassSeatPrice = lBusinessClassSeatPrice;
            FlightOwnerOwnerId = flightOwnerOwnerId;
            FlightOwner = flightOwner;
            Status = status;
        }

        public bool Equals(Flight? other)
        {
            var flight = other ?? new Flight();
            return this.FlightNumber.Equals(flight.FlightNumber);
        }
    }
}
