using BartdeBever.EventTracking.Contexts.Configurations;
using BartdeBever.EventTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace BartdeBever.EventTracking.Contexts;

public class EventTrackingDbContext : DbContext
{
    public EventTrackingDbContext(DbContextOptions<EventTrackingDbContext> options) : base(options)
    {
    }

    public DbSet<EventLog> EventLogs { get; set; }
    
    public DbSet<Application> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.UseIdentityByDefaultColumns();
        modelBuilder.ApplyConfiguration(new EventLogEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationEntityTypeConfiguration());
    }
}
