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

        //TODO try catch user already exists

        if (registerDto.IsCoach)
        {
            if (!registerDto.IsValidCoach())
            {
                //TODO return bad request
                return BadRequest(registerDto);
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