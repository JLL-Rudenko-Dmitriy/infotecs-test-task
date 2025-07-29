using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using TimescaleDomain.Timescales.Entities;

namespace Persistence;

public class TimescaleDbContext : DbContext
{
    public TimescaleDbContext(DbContextOptions<TimescaleDbContext> options)
        : base(options)
    {
    }

    public DbSet<TimescaleTable> TimescaleTables { get; set; } = null!;
    
    public DbSet<TimescaleRecord> TimescaleRecords { get; set; } = null!;
    
    public DbSet<TimescalesResultData> TimescalesResultData { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(TimescaleTableConfigurations).Assembly)
            .ApplyConfigurationsFromAssembly(typeof(TimescalesResultData).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}