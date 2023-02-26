namespace FindCoachApi.Exceptions;

public class UserAlreadyExistsException: ApplicationException
{
    public UserAlreadyExistsException(string message): base(message)
    {
        
    }
}