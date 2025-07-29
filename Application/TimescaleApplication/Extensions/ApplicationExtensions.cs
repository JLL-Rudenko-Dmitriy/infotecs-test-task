using Microsoft.Extensions.DependencyInjection;
using TimescaleApplication.Abstractions.DataReaderStrategy;
using TimescaleApplication.Contracts;
using TimescaleApplication.Services;
using TimescaleDomain.Timescales.Abstractions.Repositories;
using TimescaleDomain.TimescaleValidators.Abstractions;
using TimescaleDomain.TimescaleValidators.Entities.ValidatorChains;
using IDomainTimescaleService = TimescaleDomain.Contracts.ITimescaleService;
using DomainTimescaleService = TimescaleDomain.Services.TimescaleService;

namespace TimescaleApplication.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IValidatorChain, ValidatorChain>();
        
        services.AddScoped<IDomainTimescaleService, DomainTimescaleService>(sp =>
        {
            var validatorChain = sp.GetRequiredService<IValidatorChain>();
            return new DomainTimescaleService(validatorChain);
        });
        
        return services.AddScoped<ITimescaleService, TimescaleService>(sp =>
        {
            var timescaleService = sp.GetRequiredService<IDomainTimescaleService>();
            var dataReaderStrategy = sp.GetRequiredService<IDataReaderStrategy>();
            var timescaleTableRepository = sp.GetRequiredService<ITimescaleTableRepository>();
            var resultsRepository = sp.GetRequiredService<ITimescaleResultDataRepository>();
            
            return new TimescaleService(
                timescaleService,
                resultsRepository,
                timescaleTableRepository,
                dataReaderStrategy);
        });
    }
}