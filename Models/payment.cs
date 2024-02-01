using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simplifly.Models
{
    public class Payment : IEquatable<Payment>
    {
        [Key]
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }

        // Payment details (e.g., card number, expiry date, CVV)
        public PaymentDetails PaymentDetails { get; set; } = new PaymentDetails();
        [ForeignKey("BookingId")]
        public int BookingId { get; set; }
        // Navigation Properties
        public Booking? Booking { get; set; }

        public Payment()
        {

        }

        public Payment(int paymentId, decimal amount, DateTime paymentDate, PaymentStatus status, PaymentDetails paymentDetails, Booking booking)
        {
            PaymentId = paymentId;
            Amount = amount;
            PaymentDate = paymentDate;
            Status = status;
            PaymentDetails = paymentDetails;
            Booking = booking;
        }
        public Payment( decimal amount, DateTime paymentDate, PaymentStatus status, PaymentDetails paymentDetails, Booking booking)
        {
            Amount = amount;
            PaymentDate = paymentDate;
            Status = status;
            PaymentDetails = paymentDetails;
            Booking = booking;
        }
        public bool Equals(Payment? other)
        {
            var Payment = other ?? new Payment();
            return this.PaymentId.Equals(Payment.PaymentId) && this.BookingId.Equals(Payment.BookingId);
        }
    }

    public enum PaymentStatus
    {
        Pending,
        Successful,
        Failed
    }

    public class PaymentDetails
    {
        public int PaymentDetailsId { get; set; }

        public string CardNumber { get; set; } =string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;

    }

}
