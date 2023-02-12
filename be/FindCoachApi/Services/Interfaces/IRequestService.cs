using FindCoachApi.Entities;

namespace FindCoachApi.Services.Interfaces;

public interface IRequestService
{
    Task<List<Entities.Request>> GetRequestsByCoachId(int coachId);
    Task AddRequest(Entities.Request request);
}