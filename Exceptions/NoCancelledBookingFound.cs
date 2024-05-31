namespace Simplifly.Exceptions
{
    public class NoCancelledBookingFound:Exception
    {
        private string _message;

        public NoCancelledBookingFound()
        {
            _message = "No Cancelled Booking Found";
        }

        public override string Message => _message;
    }
}
