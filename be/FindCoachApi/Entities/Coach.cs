
namespace FindCoachApi.Entities;

public class Coach: User
{
    public string? Description { get; set; }
    public decimal HourlyRate { get; set; }
    public List<AreaExpertise> Areas { get; set; } = new();
}