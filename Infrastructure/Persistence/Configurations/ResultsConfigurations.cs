using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimescaleDomain.Timescales.Abstractions.Entities;
using TimescaleDomain.Timescales.Entities;
using TimescaleDomain.Timescales.ValueObjects;

namespace Persistence.Configurations;

public class ResultsConfigurations : IEntityTypeConfiguration<TimescalesResultData>
{
    public void Configure(EntityTypeBuilder<TimescalesResultData> builder)
    {
        ConfigureResults(builder);
    }
    
    private void ConfigureResults(EntityTypeBuilder<TimescalesResultData> builder)
    {
        builder.ToTable("Results");
        
        builder.HasKey(r => r.Id);
        
        builder
            .Property(r => r.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .HasConversion<Guid>();

        builder.HasOne<TimescaleTable>()
            .WithOne()
            .HasForeignKey<TimescalesResultData>("timescale_id");
        
        builder.Property<Guid>("timescale_id")
            .HasColumnName("timescale_id")
            .IsRequired();
        
        builder
            .Property(t => t.TimeDelta)
            .HasColumnName("time_delta")
            .HasConversion(
                seconds => seconds.Value,
                doubleValue => new Seconds(doubleValue));

        builder
            .Property(t => t.FirstOperationDateTime)
            .HasColumnName("first_operation_datetime")
            .HasConversion(
                dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToUniversalTime(),
                pgDateTime => pgDateTime);

        builder
            .Property(t => t.AverageExecutionTime)
            .HasColumnName("average_execution_time");

        builder
            .Property(t => t.AverageValue)
            .HasColumnName("average_value");

        builder
            .Property(t => t.MedianValue)
            .HasColumnName("median_value");

        builder
            .Property(t => t.MaxValue)
            .HasColumnName("max_value");

        builder
            .Property(t => t.MinValue)
            .HasColumnName("min_value");
    }
}