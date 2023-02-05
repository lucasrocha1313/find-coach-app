using FindCoachApi.Data;
using FindCoachApi.Entities;
using FindCoachApi.Services.Interfaces;

namespace FindCoachApi.Services;

public class AuthService: IAuthService
{
    private readonly DataContext _context;
    private readonly ILogger<AuthService> _logger;

    public AuthService(DataContext context, ILogger<AuthService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User> RegisterUser(User user)
    {
        if (_context.Users.Any(u => u.UserName == user.UserName))
        {
            //TODO throw bad request exception
            _logger.LogWarning($"User with username {user.UserName} already exists");
            return null;
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task ChangeToCoach(Coach coach)
    {
        if (!_context.Users.Any(u => u.Id == coach.Id))
        {
            _logger.LogError($"No user with id {coach.Id}");
            return;
        }

        var coachOnDatabase = _context.Coaches.First(c => c.Id == coach.Id);
        coachOnDatabase.HourlyRate = coach.HourlyRate;
        coachOnDatabase.Description = coach.Description;
        coachOnDatabase.Areas = coach.Areas;

        await _context.SaveChangesAsync();
    }
}