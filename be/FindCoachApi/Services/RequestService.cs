using FindCoachApi.Data;
using FindCoachApi.Entities;
using FindCoachApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindCoachApi.Services;

public class RequestService: ServiceBase, IRequestService
{
    public RequestService(DataContext context) : base(context)
    {
    }
    public async Task<List<Request>> GetRequestsByCoachId(int coachId)
    {
        return await _context.Requests.Where(r => r.CoachId == coachId).ToListAsync();
    }

    public async Task<Request> AddRequest(Request request)
    {
        await _context.Requests.AddAsync(request);
        await _context.SaveChangesAsync();
        return request;
    }
}