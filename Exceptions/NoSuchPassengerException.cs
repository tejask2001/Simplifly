namespace Simplifly.Exceptions
{
    public class NoSuchPassengerException : Exception
    {
        private readonly string message;
        public NoSuchPassengerException()
        {
            message = "No Passenger found with given details";
        }
        public override string Message => message;
    }
}
