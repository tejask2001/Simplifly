namespace Simplifly.Exceptions
{
    public class FlightAlreadyPresentException:Exception
    {
        private readonly string message;
        public FlightAlreadyPresentException()
        {
            message = "Flight is already present.";
        }

        public override string Message => message;
    }
}
