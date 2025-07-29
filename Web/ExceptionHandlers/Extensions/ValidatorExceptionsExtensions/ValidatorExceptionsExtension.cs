using ExceptionHandlers.ExceptionHandlers.ValidatorExceptionHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace ExceptionHandlers.Extensions.ValidatorExceptionsExtensions;

public static class ValidatorExceptionsExtension
{
    public static IServiceCollection AddValidatorExceptionHandlers(this IServiceCollection services)
    {
        return services
            .AddExceptionHandler<InvalidDateFormatExceptionHandler>()
            .AddExceptionHandler<NullExecutionTimeExceptionHandler>()
            .AddExceptionHandler<TimescaleValidatorExceptionHandler>();
    }
}