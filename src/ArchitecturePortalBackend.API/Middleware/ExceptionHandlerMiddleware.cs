using Microsoft.AspNetCore.Mvc;
using ArchitecturePortalBackend.BusinessLogic.Exceptions;
using System.Diagnostics;
using System.Net;

namespace ArchitecturePortalBackend.API.Middleware;

internal class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            try
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
            catch (Exception innerException)
            {
                _logger.LogError(0, innerException, "Exception handling error");
            }

            throw;
        }
    }

    private static async Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        
        var index = 0;
        var stackTraceDictionary = exception.StackTrace?.Split("\n").ToDictionary(s => index++, s => s.Trim().Trim('\r'));
        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = statusCode,
            Extensions =
            {
                ["InnerException"] = exception.InnerException?.Message,
                ["StackTrace"] = stackTraceDictionary
            }
        };

        var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private static int GetStatusCode(Exception exception)
    {
        if (exception is NotFoundException)
        {
            return (int)HttpStatusCode.NotFound;
        }

        return (int)HttpStatusCode.InternalServerError;
    }
}