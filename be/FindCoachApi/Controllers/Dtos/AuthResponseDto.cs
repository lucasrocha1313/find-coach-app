namespace FindCoachApi.Controllers.Dtos;

public class AuthResponseDto
{
    public int AuthId { get; set; }
    public string Token { get; set; }
    public bool IsCoach { get; set; }
}