using FindCoachApi.Entities;

namespace FindCoachApi.Services.Interfaces;

public interface IAuthService
{
    Task RegisterUser(User user);
    Task ChangeToCoach(Coach coach);
}