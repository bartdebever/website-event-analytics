using BartdeBever.EventTracking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartdeBever.EventTracking.Contexts.Configurations;

public class EventLogEntityTypeConfiguration : IEntityTypeConfiguration<EventLog>
{
    public void Configure(EntityTypeBuilder<EventLog> builder)
    {
        builder.ToTable("event_logs");
        builder.HasKey(eventLog => eventLog.Id);
        builder.Property(eventLog => eventLog.Id).ValueGeneratedOnAdd();
        builder.Property(eventLog => eventLog.EventName).IsRequired();
        builder.Property(eventLog => eventLog.SessionId).IsRequired();
        builder.Property(eventLog => eventLog.CreatedAt).IsRequired();
        builder.Property(eventLog => eventLog.Url).HasMaxLength(2048);

        // Store JSON data as jsonb in Postgres
        builder.Property(eventLog => eventLog.Data).HasColumnType("jsonb");
    }
}