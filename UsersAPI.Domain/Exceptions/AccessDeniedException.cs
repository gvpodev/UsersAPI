namespace UsersAPI.Domain.Exceptions;

public class AccessDeniedException : Exception
{
    public override string Message => "Access was denied. Invalid user.";
}