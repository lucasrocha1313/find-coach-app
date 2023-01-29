using System.ComponentModel;

namespace FindCoachApi.Enums;

public enum AreasExpertise
{
    [Description("Frontend Development")]
    FRONTEND = 1,
    [Description("Backend Development")]
    BACKEND,
    [Description("Career Advisor")]
    CAREER
}