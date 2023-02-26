using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FindCoachApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FindCoachApi.Helpers;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(IConfiguration config, RequestDelegate next, ILogger<JwtMiddleware> logger)
    {
        _config = config;
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IAuthService authService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            await AttachUserToContext(context, authService, token);
        }
        
        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IAuthService authService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("Token").Value);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);
            
            var jwtToken = (JwtSecurityToken)validatedToken;
            var email = jwtToken.Claims.First(x => x.Type == "email").Value;
            context.Items["Auth"] = await authService.GetAuthByEmailAsync(email);
        }
        catch(Exception e)
        {
            _logger.LogWarning("User not logged in", e);
        }
    }
}