﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Simplifly.Models
{
    public class PassengerBooking : IEquatable<PassengerBooking>
    {
        [Key]
        public int Id { get; set; }
        public int? BookingId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table
        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        public int? PassengerId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table
        [ForeignKey("PassengerId")]
        public Passenger? Passenger { get; set; }

        public int? SeatId { get; set; }
        //This one is just for navigation and will not be created as an attribute in table
        [ForeignKey("SeatId")]
        public SeatDetail? SeatDetail { get; set; }


        public PassengerBooking()
        {
            Id = 0;
        }
        public PassengerBooking(int id)
        {
            Id = id;
           
        }

        public PassengerBooking(int id, int? bookingId, int? passengerId, int? seatId) : this(id)
        {
            BookingId = bookingId;
            PassengerId = passengerId;
            SeatId = seatId;
        }

        public bool Equals(PassengerBooking? other)
        {
            var passengerBooking = other ?? new PassengerBooking();
            return this.Id.Equals(passengerBooking.Id);
        }

    }
}
