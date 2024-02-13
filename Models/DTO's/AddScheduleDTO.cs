namespace Simplifly.Models.DTO_s
{
    public class AddScheduleDTO
    {
        public int RouteId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
