using BartdeBever.EventTracking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartdeBever.EventTracking.Contexts.Configurations;

public class ApplicationEntityTypeConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("applications");
        builder.HasKey(eventLog => eventLog.Id);
        builder.Property(eventLog => eventLog.Name).IsRequired();
        builder.Property(eventLog => eventLog.ApiKey).IsRequired();
        builder.Property(eventLog => eventLog.SchemaName).IsRequired();
        builder.HasIndex(eventLog => eventLog.ApiKey).IsUnique();
    }
}