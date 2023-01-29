using System.ComponentModel.DataAnnotations;

namespace FindCoachApi.Controllers.Dtos;

public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "The username value cannot exceed 50 and be lower than 6 characters.")]
    public string? UserName { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "The first name value cannot exceed 30 and be lower than 6 characters. ")]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "The last name value cannot exceed 30 and be lower than 6 characters. ")]
    public string? LastName { get; set; }
    public bool IsCoach { get; set; }
    public string? Description { get; set; }
    public decimal? HourlyRate { get; set; }
    public List<int>? IdsAreas { get; set; }

    public bool IsValidCoach()
    {
        return Description != null && HourlyRate != null && IdsAreas != null;
    }
}