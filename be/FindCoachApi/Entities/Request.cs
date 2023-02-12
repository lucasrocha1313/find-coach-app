using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindCoachApi.Entities;

public class Request
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int CoachId { get; set; }
    [Required]
    [StringLength(200)]
    public string? UserEmail { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string? Message { get; set; }
}