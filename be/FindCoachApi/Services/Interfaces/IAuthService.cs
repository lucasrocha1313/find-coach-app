using FindCoachApi.Controllers.Dtos;
using FindCoachApi.Entities;

namespace FindCoachApi.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterUser(User user);
    Task ChangeToCoach(Coach coach);
    Task SignUp(string email, string password);
    Task<AuthResponseDto?> Login(string email, string password);
    Task<Auth?> GetAuthByEmailAsync(string email);
}