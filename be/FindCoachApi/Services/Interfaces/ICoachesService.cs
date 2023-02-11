using FindCoachApi.Entities;

namespace FindCoachApi.Services.Interfaces;

public interface ICoachesService
{
    Task<List<Coach>> GetCoaches();
    Task<Coach> GetCoachById(int id);
}