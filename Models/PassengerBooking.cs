using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Simplifly.Models
{
    public class PassengerBooking : IEquatable<PassengerBooking>
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int? BookingId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table
        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        public int? PassengerId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table
        [ForeignKey("PassengerId")]
        public Passenger? Passenger { get; set; }


        public PassengerBooking()
        {
            Id = 0;
        }
        public PassengerBooking(int id, DateTime dateOfApp)
        {
            Id = id;
            Date = dateOfApp;
        }

        public PassengerBooking(DateTime dateOfApp)
        {
            Date = dateOfApp;
        }

        public PassengerBooking(int id, DateTime date, int? doctorId, int? patientId) : this(id, date)
        {
            BookingId = doctorId;
            PassengerId = patientId;
        }

        public bool Equals(PassengerBooking? other)
        {
            var passengerBooking = other ?? new PassengerBooking();
            return this.Id.Equals(passengerBooking.Id);
        }

    }
}
