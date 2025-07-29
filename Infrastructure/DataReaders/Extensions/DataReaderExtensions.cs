using Microsoft.Extensions.DependencyInjection;
using TimescaleApplication.Abstractions.DataReaderStrategy;

namespace DataReaders.Extensions;

public static class DataReaderExtensions
{
    public static IServiceCollection AddDataReaders(this IServiceCollection services)
    {
        return services.AddScoped<IDataReaderStrategy, DataReaderStrategy>();
    }
}