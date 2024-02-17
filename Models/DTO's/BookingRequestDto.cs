using Simplifly.Models;

namespace Simplifly.Controllers
{
    public class BookingRequestDto
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingTime { get; set; }
       //ublic double TotalPrice { get; set; }
        public List<int> PassengerIds { get; set; }

        public List<string> SelectedSeats { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

    }
}