using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindCoachApi.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string? UserName { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string? LastName { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
    
}