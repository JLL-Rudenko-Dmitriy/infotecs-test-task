using ExceptionHandlers.ExceptionHandlers;
using ExceptionHandlers.Extensions.ValidatorExceptionsExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace ExceptionHandlers.Extensions;

public static class ExceptionHandlerExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        return services
            .AddProblemDetails()
            .AddExceptionHandler<DataReaderExceptionHandler>()
            .AddExceptionHandler<DomainDataReaderExceptionHandler>()
            .AddValidatorExceptionHandlers();

    }
}