using FindCoachApi.Data;

namespace FindCoachApi.Services;

public abstract class ServiceBase
{
    protected readonly DataContext _context;

    public ServiceBase(DataContext context)
    {
        _context = context;
    }
}