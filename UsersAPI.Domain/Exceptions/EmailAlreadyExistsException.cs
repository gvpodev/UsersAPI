namespace UsersAPI.Domain.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string email)
            : base($"The e-mail {email} is already in use.")
        {
        }
    }
}
