using FindCoachApi.Data;
using FindCoachApi.Entities;
using FindCoachApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindCoachApi.Services;

public class Coaches: ServiceBase, ICoachesService
{

    public Coaches(DataContext context): base(context)
    {
    }

    public async Task<List<Coach>> GetCoaches()
    {
        return await _context.Coaches.Include(c => c.Areas).ToListAsync();
    }

    public async Task<Coach?> GetCoachById(int id)
    {
        return await _context.Coaches.Include(c => c.Areas).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> IsCoachRegistered(int id)
    {
        return await _context.Coaches.AnyAsync(c => c.Id == id);
    }
}