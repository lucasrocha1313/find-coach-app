using FindCoachApi.Controllers.Dtos;
using FindCoachApi.Entities;
using FindCoachApi.Enums;
using FindCoachApi.Extensions;
using FindCoachApi.Helpers;
using FindCoachApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FindCoachApi.Controllers;

[ApiController]
[Route("api/register")]
public class RegisterController: ControllerBase
{
    private readonly IAuthService _authService;

    public RegisterController(IAuthService authService)
    {
        _authService = authService;
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var userOnDb = await _authService.GetUserByAuthId(registerDto.AuthId);

        if (registerDto.IsCoach)
        {
            if (userOnDb as Coach != null)
            {
                return BadRequest("Coach already registered for this authentication");
            }

            if (!registerDto.IsValidCoach())
            {
                return BadRequest("Invalid coach data");
            }

            var coach = new Coach
            {
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                HourlyRate = registerDto.HourlyRate.Value,
                Description = registerDto.Description,
                Areas = registerDto.IdsAreas.Select(id => new AreaExpertise
                {
                    Title = ((AreasExpertise)id).GetDescription(),
                    Name = ((AreasExpertise)id).ToString().ToLower()
                }).ToList()
            };
            await _authService.RegisterUser(coach);
            
            return Ok(coach);
        }

        if(userOnDb != null)
            return BadRequest("User already registered for this authentication");

        var user = new User
        {
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };
        await _authService.RegisterUser(user);
        return Ok(user);
    }
}