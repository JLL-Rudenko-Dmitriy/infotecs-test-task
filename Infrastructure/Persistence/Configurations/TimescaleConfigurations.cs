using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringCoder;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace Persistence.Configurations;

public class TimescaleTableConfigurations : IEntityTypeConfiguration<TimescaleTable>
{
    public void Configure(EntityTypeBuilder<TimescaleTable> builder)
    {
        ConfigureTimescaleTable(builder);
        ConfigureTimescaleResults(builder);
    }

    private void ConfigureTimescaleTable(EntityTypeBuilder<TimescaleTable> builder)
    {
        builder.ToTable("TimeScales");

        builder
            .HasKey(t => t.Id)
            .HasName("id");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasConversion<Guid>();
        
        builder
            .Property(t => t.Name)
            .HasColumnName("name")
            .HasConversion(
                v => v.ToBase64(),
                v => v.FromBase64());
    }

    private void ConfigureTimescaleResults(EntityTypeBuilder<TimescaleTable> builder)
    {
        builder.OwnsMany(t => t.Records, rb =>
        {
            rb.ToTable("Values");
            
            rb.WithOwner().HasForeignKey("timescale_id");
            
            
            rb.HasKey(r => r.Id);
            
            rb
                .Property(r => r.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .HasConversion<Guid>();

            rb
                .Property(r => r.Date)
                .HasColumnName("date_time")
                .HasConversion(
                    dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToUniversalTime(),
                    pgDateTime => pgDateTime);
            
            rb
                .Property(r => r.ExecutionTime)
                .HasColumnName("execution_time")
                .HasConversion(
                    seconds => seconds.Value,
                    doubleValue => new Seconds(doubleValue));

            rb
                .Property(r => r.Value)
                .HasColumnName("value");
        });
    }
}