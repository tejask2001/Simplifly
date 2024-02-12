using Simplifly.Models;

namespace Simplifly.Controllers
{
    public class BookingRequestDto
    {
        public string FlightId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingTime { get; set; }
       //ublic double TotalPrice { get; set; }
        public List<int> PassengerIds { get; set; }

        public List<int> SelectedSeats { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

    }
}