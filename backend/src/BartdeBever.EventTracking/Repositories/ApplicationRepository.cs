using BartdeBever.EventTracking.Contexts;
using BartdeBever.EventTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace BartdeBever.EventTracking.Repositories;

public class ApplicationRepository
{
    private readonly EventTrackingDbContext _context;

    public ApplicationRepository(EventTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<Application?> GetByApiKeyAsync(string apiKey)
    {
        return await _context.Applications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ApiKey == apiKey);
    }
}
