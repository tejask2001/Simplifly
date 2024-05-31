using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class CancelledBooking
    {
        [Key]
        public int Id { get; set; }
        public string passengerName { get; set; }
        public int scheduleId { get; set; }
        [ForeignKey("scheduleId")]
        public Schedule? Schedule { get; set; }
        public int paymentId { get; set; }
        [ForeignKey("paymentId")]
        public Payment? Payment { get; set; }

        public double RefundAmount { get; set; }
        public string RefundStatus { get; set; } = string.Empty;
        public string cardNumber { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Customer? Customer { get; set; }

        public CancelledBooking()
        {
            
        }

        public CancelledBooking(int id, string passengerName, int scheduleId, Schedule? schedule, int paymentId, Payment? payment, double refundAmount, string refundStatus, string cardNumber, int userId, Customer? customer)
        {
            Id = id;
            this.passengerName = passengerName;
            this.scheduleId = scheduleId;
            Schedule = schedule;
            this.paymentId = paymentId;
            Payment = payment;
            RefundAmount = refundAmount;
            RefundStatus = refundStatus;
            this.cardNumber = cardNumber;
            UserId = userId;
            Customer = customer;
        }
    }
}
