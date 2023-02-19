using FindCoachApi.Entities;

namespace FindCoachApi.Services.Interfaces;

public interface IRequestService
{
    Task<List<Request>> GetRequestsByCoachId(int coachId);
    Task<Request> AddRequest(Request request);
}