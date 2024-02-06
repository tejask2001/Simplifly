namespace Simplifly.Exceptions
{
    [Serializable]
    internal class InvlidUuserException : Exception
    {
        string message;
        public InvlidUuserException()
        {
            message = "Invalid username or password";
        }

        public override string Message => message;
    }
}
