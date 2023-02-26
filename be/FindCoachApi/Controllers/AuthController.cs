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
        public async Task<ActionResult> AuthUser(AuthDto auth)
        {
            try
            {
                await _authService.SignUp(auth.Email, auth.Password);
                return Ok("User signed up successfully");
            }
            catch (UserAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<string>> Login(string email, string password)
        {
            var token = await _authService.Login(email, password);
            if (token == null)
                return BadRequest("Something went wrong");

            return Ok(token);
        }
    }
}