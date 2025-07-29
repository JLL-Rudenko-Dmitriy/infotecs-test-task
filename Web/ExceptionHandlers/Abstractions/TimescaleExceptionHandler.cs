using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimescaleExceptions.Abstractions;

namespace ExceptionHandlers.Abstractions;

public class TimescaleExceptionHandler<T> : IExceptionHandler
    where T : Exception
{
    protected virtual string SpecializedExceptionTitle { get; } = "Unhandled Exception";

    protected virtual int SpecializedExceptionStatusCode { get; } = 500;

    protected virtual string SpecializedExceptionMessageDetails { get; } = "Can't handle exception";
    
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        return exception switch
        {
            ITimescaleException timescaleException => await HandleTimescaleExceptionAsync(httpContext,
                timescaleException, cancellationToken),
            T => await HandleSpecializedException(httpContext, cancellationToken),
            _ => false
        };
    }

    private async ValueTask<bool> HandleTimescaleExceptionAsync(
        HttpContext httpContext,
        ITimescaleException exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = CreateTimescaleProblemDetails(exception);

        var json = ToJson(problemDetails);
        const string contentType = "application/problem+json";

        httpContext.Response.ContentType = contentType;
        httpContext.Response.StatusCode = exception.StatusCode;

        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }
    
    private async ValueTask<bool> HandleSpecializedException(
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var problemDetails = CreateProblemDetails();

        var json = ToJson(problemDetails);
        const string contentType = "application/problem+json";

        httpContext.Response.ContentType = contentType;
        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails()
    {
        var problemDetails = new ProblemDetails
        {
            Title = SpecializedExceptionTitle,
            Detail = SpecializedExceptionMessageDetails,
            Status = SpecializedExceptionStatusCode,
        };

        return problemDetails;
    }

    protected virtual ProblemDetails CreateTimescaleProblemDetails(in ITimescaleException exception)
    {
        var baseException = (ITimescaleException)exception;

        var problemDetails = new ProblemDetails();
        var errorTitle = exception.Title;
        var errorDetails = exception.MessageDetails;
        var errorCode = exception.StatusCode;

        if (string.IsNullOrEmpty(errorTitle))
        {
            errorTitle = baseException.Title;
        }

        if (string.IsNullOrEmpty(errorDetails))
        {
            errorDetails = baseException.MessageDetails;
        }

        problemDetails.Title = errorTitle;
        problemDetails.Detail = errorDetails;
        problemDetails.Status = errorCode;

        return problemDetails;
    }
    
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
    
    private string ToJson(in ProblemDetails problemDetails)
    {
        try
        {
            return JsonSerializer.Serialize(problemDetails, SerializerOptions);
        }
        catch (Exception)
        {
        }

        return string.Empty;
    }
}