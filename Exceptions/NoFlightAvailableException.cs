namespace Simplifly.Exceptions
{
    public class NoFlightAvailableException : Exception
    {
        private readonly string message;
        public NoFlightAvailableException()
        {
            message = "No flight available";
        }
        public override string Message => message;
    }
}