using FindCoachApi.Enums;

namespace FindCoachApi.Entities;

public class Coach: User
{
    public string? Description { get; set; }
    public decimal HourlyRate { get; set; }
    public AreasExpertise Areas { get; set; }
}