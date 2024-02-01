using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Booking:IEquatable<Booking>
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        [ForeignKey("FlightNumber")]
        public Flight Flight { get; set; }
        public string SeatNumber { get; set; }
        [ForeignKey("SeatNumber")]
        public SeatDetail SeatDetail { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public DateTime BookingTime { get; set; }

        public Booking()
        {
            
        }

        public Booking(int id, string flightNumber, Flight flight, string seatNumber, SeatDetail seatDetail, int customerId, DateTime bookingTime)
        {
            Id = id;
            FlightNumber = flightNumber;
            Flight = flight;
            SeatNumber = seatNumber;
            SeatDetail = seatDetail;
            UserId = customerId;
            BookingTime = bookingTime;
        }

        public bool Equals(Booking? other)
        {
            var booking = other ?? new Booking();
            return this.Id.Equals(booking.Id);
        }
    }
}
