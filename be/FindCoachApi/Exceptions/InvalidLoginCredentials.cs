namespace FindCoachApi.Exceptions;

public class InvalidLoginCredential: ApplicationException
{
    public InvalidLoginCredential(string message): base(message)
    {
        
    }
}