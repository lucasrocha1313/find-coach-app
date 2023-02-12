using System.ComponentModel.DataAnnotations;

namespace FindCoachApi.Controllers.Dtos;

public class RequestToAddDto
{
    [Required]
    public int CoachId { get; set; }
    [Required]
    [StringLength(200)]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? UserEmail { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string? Message { get; set; }
}