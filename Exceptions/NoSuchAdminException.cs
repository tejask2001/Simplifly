namespace Simplifly.Exceptions
{
    public class NoSuchAdminException : Exception
    {
        private readonly string message;
        public NoSuchAdminException()
        {
            message = "No Admin found with given details";
        }
        public override string Message => message;
    }
}
