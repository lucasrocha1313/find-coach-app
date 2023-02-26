using System.ComponentModel.DataAnnotations;

namespace FindCoachApi.Controllers.Dtos;

public class AuthDto
{
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(100, MinimumLength = 6, ErrorMessage = "The password value be lower than 6 characters.")]
    public string Password { get; set; }
}