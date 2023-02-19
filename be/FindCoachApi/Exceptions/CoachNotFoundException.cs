namespace FindCoachApi.Exceptions;

public class CoachNotFoundException: ApplicationException
{
    public CoachNotFoundException(string message): base(message)
    {
        
    }
}