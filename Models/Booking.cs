using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Booking:IEquatable<Booking>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FlightId")]
        public string FlightId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table
        public Flight? Flight { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public DateTime BookingTime { get; set; }
        public double TotalPrice { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<SeatDetail> Seats { get; set; }

        public Booking()   
        {
            Id = 0;

        }
        public Booking( string flightId, int userId, DateTime bookingTime, double totalPrice)
        {
            FlightId = flightId;
            UserId = userId;
            BookingTime = bookingTime;
            TotalPrice = totalPrice;
        }

        public Booking(int id, string flightId, int userId, DateTime bookingTime, double totalPrice)
        {
            Id = id;
            FlightId = flightId;
            UserId = userId;
            BookingTime = bookingTime;
            TotalPrice = totalPrice;
        }

        public bool Equals(Booking? other)
        {
            var booking = other ?? new Booking();
            return this.Id.Equals(booking.Id);
        }
    }
}
