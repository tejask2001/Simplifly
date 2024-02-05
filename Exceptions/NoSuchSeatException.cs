namespace Simplifly.Exceptions
{
    public class NoSuchSeatException:Exception
    {
        private readonly string message;
        public NoSuchSeatException()
        {
            message = "No seat found with given details";
        }
        public override string Message => message;
    }
}
