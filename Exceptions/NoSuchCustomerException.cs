namespace Simplifly.Exceptions
{
    public class NoSuchCustomerException : Exception
    {
        private readonly string message;
        public NoSuchCustomerException()
        {
            message = "No Customer found with given details";
        }
        public override string Message => message;
    }
}
