using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Booking:IEquatable<Booking>
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public Schedule? Schedule { get; set; }
        public string SeatNumber { get; set; }
        [ForeignKey("SeatNumber")]
        public SeatDetail? SeatDetail { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public DateTime BookingTime { get; set; }

        public Booking()   
        {
            Id = 0;

        }

        public Booking(int id, int scheduleId, Schedule schedule, string seatNumber, SeatDetail seatDetail, int customerId, DateTime bookingTime)
        {
            Id = id;
            ScheduleId = scheduleId;
            Schedule = schedule;
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
