namespace Simplifly.Exceptions
{
    public class NoSuchScheduleException:Exception
    {
        private readonly string message;
        public NoSuchScheduleException()
        {
            message = "No schedule found with given details";
        }
        public override string Message => message;
    }
}
