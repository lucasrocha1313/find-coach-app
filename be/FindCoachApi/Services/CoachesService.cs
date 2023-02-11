using FindCoachApi.Data;
using FindCoachApi.Entities;
using FindCoachApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindCoachApi.Services;

public class CoachesService: ICoachesService
{
    private readonly DataContext _context;

    public CoachesService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Coach>> GetCoaches()
    {
        return await _context.Coaches.Include(c => c.Areas).ToListAsync();
    }

    public async Task<Coach> GetCoachById(int id)
    {
        return await _context.Coaches.Include(c => c.Areas).FirstAsync(c => c.Id == id);
    }
}