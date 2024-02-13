using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class SeatDetail:IEquatable<SeatDetail>
    {
        [Key]
        public int Id { get; set; }

        public String FlightId { get; set; }

        [ForeignKey("FlightId")]
        public Flight? Flight { get; set; }

        public string SeatNumber { get; set; }

        public bool IsBooked { get; set; }

        public SeatDetail()
        {
            
        }
        public SeatDetail( string flightId, string seatNumber, bool isBooked)
        {
            FlightId = flightId;
            SeatNumber = seatNumber;
            IsBooked = isBooked;
        }

        public SeatDetail(int id, string flightId, string seatNumber, bool isBooked)
        {
            Id = id;
            FlightId = flightId;
            SeatNumber = seatNumber;
            IsBooked = isBooked;
        }

        public bool Equals(SeatDetail? other)
        {
            var seatDetail = other ?? new SeatDetail();
            return this.SeatNumber.Equals(seatDetail.SeatNumber);
        }
    }
}
