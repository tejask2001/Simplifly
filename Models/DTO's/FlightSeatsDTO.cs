using System.Diagnostics.CodeAnalysis;

namespace Simplifly.Models.DTO_s
{
    [ExcludeFromCodeCoverage]
    public class FlightSeatsDTO
    {
        public string FlightNumber { get; set; }
        public int TotalEconomySeats { get; set; }
        public int TotalPremiumEconomySeats { get; set; }
        public int TotalBusinessClassSeats { get; set; }
    }
}
