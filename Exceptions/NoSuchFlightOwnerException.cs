namespace Simplifly.Exceptions
{
    public class NoSuchFlightOwnerException:Exception
    {
        private readonly string message;
        public NoSuchFlightOwnerException()
        {
            message = "No Flight owner found with given details";
        }
        public override string Message => message;
    }
}
