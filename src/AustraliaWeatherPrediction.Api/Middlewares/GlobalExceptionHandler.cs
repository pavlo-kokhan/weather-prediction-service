using System;
using System.Threading;
using System.Threading.Tasks;
using AustraliaWeatherPrediction.Api.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AustraliaWeatherPrediction.Api.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IHostEnvironment hostEnvironment,
        IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError("Unhandled exception occured: {Exception}", exception);
        
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var exceptionDetailsAllowed = _hostEnvironment.IsDevelopment() || _hostEnvironment.IsDebug();

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exceptionDetailsAllowed ? exception : null,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Internal server error",
                Detail = exceptionDetailsAllowed ? exception.Message : null
            }
        });
    }
}