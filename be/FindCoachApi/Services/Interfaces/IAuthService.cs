using FindCoachApi.Entities;

namespace FindCoachApi.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterUser(User user);
    Task ChangeToCoach(Coach coach);
}