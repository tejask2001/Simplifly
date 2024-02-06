namespace Simplifly.Exceptions
{
    public class NoSuchFlightException:Exception
    {
        private readonly string message;
        public NoSuchFlightException()
        {
            message = "No flight found with given details";
        }
        public override string Message => message;
    }
}
