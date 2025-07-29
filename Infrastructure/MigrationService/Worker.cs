using Microsoft.EntityFrameworkCore;
using Persistence;

namespace MigrationService;

public class Migrator : BackgroundService
{
    private readonly ILogger<Migrator> _logger;
    private readonly IServiceScopeFactory  _scopeFactory;
    public Migrator(ILogger<Migrator> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Migrate();
    }

    private async Task Migrate()
    { 
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TimescaleDbContext>();
        
        _logger.LogInformation("Migrating TimescaleDbContext");
        
        await context.Database.MigrateAsync();
        
        _logger.LogInformation("Migrated");
    }
}