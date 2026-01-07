using BartdeBever.EventTracking.Contexts;
using BartdeBever.EventTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace BartdeBever.EventTracking.Repositories;

public interface IEventLogRepository
{
    Task<EventLog> CreateAsync(EventLog eventLog);

    Task<EventLog?> GetByIdAsync(long id);
}

public class EventLogRepository : IEventLogRepository
{
    private readonly EventTrackingDbContext _context;

    public EventLogRepository(EventTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<EventLog> CreateAsync(EventLog eventLog)
    {
        _context.EventLogs.Add(eventLog);
        await _context.SaveChangesAsync();
        return eventLog;
    }

    public async Task<EventLog?> GetByIdAsync(long id)
    {
        return await _context.EventLogs.FindAsync(id);
    }
    
}
