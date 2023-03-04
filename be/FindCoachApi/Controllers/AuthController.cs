using FindCoachApi.Controllers.Dtos;
using FindCoachApi.Exceptions;
using FindCoachApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FindCoachApi.Controllers
{
    [ApiController]
    [Route("api/auth/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponseDto>> AuthUser(AuthDto auth)
        {
            try
            {
                await _authService.SignUp(auth.Email, auth.Password);
                var authResponse = await _authService.Login(auth.Email, auth.Password);
                return Ok(authResponse);
            }
            catch (UserAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(AuthDto auth)
        {
            try
            {
                var authResponse = await _authService.Login(auth.Email, auth.Password);
                if (authResponse == null)
                    return BadRequest("Something went wrong");

                return Ok(authResponse);
            }
            catch (InvalidLoginCredential e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}