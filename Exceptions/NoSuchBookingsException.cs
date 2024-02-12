using System.Linq.Expressions;

namespace Simplifly.Exceptions
{
    public class NoSuchBookingsException:Exception
    {
        private readonly string message;
        public NoSuchBookingsException()
        {
            message = "No flight found with given details";
        }
        public override string Message => message;
    }
}
