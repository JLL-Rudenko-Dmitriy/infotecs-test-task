using EnvLoader;
using Microsoft.EntityFrameworkCore;
using MigrationService;
using Persistence;

class Program
{
    public static Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING__POSTGRES");

        builder.Services.AddDbContext<TimescaleDbContext>(options => 
            options
                .UseNpgsql(
                    connectionString ?? throw new ArgumentNullException(connectionString), npgsqlOptions =>
                        npgsqlOptions.MigrationsAssembly("MigrationService")));

        builder.Services.AddHostedService<Migrator>(
            sp =>
            {
                var logger = sp.GetRequiredService<ILogger<Migrator>>();
                var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new Migrator(logger, serviceScopeFactory);
            });

        var host = builder.Build();
        host.Run();
        return Task.CompletedTask;
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        DotEnv.Load("../../environment/.env");
        
        Console.WriteLine("CreateHostBuilder CALLED");
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING__POSTGRES");
                
                services.AddDbContext<TimescaleDbContext>(optBuilder => optBuilder
                    .UseNpgsql(
                        connectionString ?? throw new ArgumentNullException(connectionString), npgsqlOptions =>
                            npgsqlOptions.MigrationsAssembly("MigrationService")));
            });
    }
}




