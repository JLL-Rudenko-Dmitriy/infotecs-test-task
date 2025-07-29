using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using TimescaleDomain.Timescales.Abstractions.Repositories;

namespace Persistence.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string  connectionString)
    {

        return services
            .AddDbContext<TimescaleDbContext>(opt => opt.UseNpgsql(connectionString))
            .AddScoped<ITimescaleTableRepository, TimescaleTableRepository>()
            .AddScoped<ITimescaleResultDataRepository, TimescaleResultDataRepository>();
    }
}