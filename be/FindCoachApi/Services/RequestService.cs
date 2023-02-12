using FindCoachApi.Data;
using FindCoachApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindCoachApi.Services;

public class Request: ServiceBase, IRequestService
{
    public Request(DataContext context) : base(context)
    {
    }
    public async Task<List<Entities.Request>> GetRequestsByCoachId(int coachId)
    {
        return await _context.Requests.Where(r => r.CoachId == coachId).ToListAsync();
    }

    public async Task AddRequest(Entities.Request request)
    {
        await _context.Requests.AddAsync(request);
        await _context.SaveChangesAsync();
    }
}