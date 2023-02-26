using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FindCoachApi.Data;
using FindCoachApi.Entities;
using FindCoachApi.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using FindCoachApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FindCoachApi.Services;

public class AuthService: ServiceBase, IAuthService
{
    private readonly ILogger<Auth> _logger;
    private readonly IConfiguration _config;

    public AuthService(DataContext context, ILogger<Auth> logger, IConfiguration config): base(context)
    {
        _logger = logger;
        _config = config;
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

    public async Task<string?> Login(string email, string password)
    {
        var auth = await _context.Auths.FirstOrDefaultAsync(c => c.Email == email);
        if (auth == null || !VerifyPasswordHash(password, auth.PasswordHash, auth.PasswordSalt))
            return null;

        return GenerateToken(email);
    }

    public async Task SignUp(string email, string password)
    {
        if (_context.Auths.Any(a => a.Email == email))
        {
            throw new UserAlreadyExistsException("Email Already used");
        }
        CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        var auth = new Auth
        {
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        _context.Auths.Add(auth);
        await _context.SaveChangesAsync();
    }

    public async Task<Auth?> GetAuthByEmailAsync(string email)
    {
        return await _context.Auths.FirstOrDefaultAsync(a => a.Email == email);
    }
    
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(passwordHash);
    }

    private string? GenerateToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetSection("Token").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}