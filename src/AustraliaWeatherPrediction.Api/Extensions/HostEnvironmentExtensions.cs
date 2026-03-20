using Microsoft.Extensions.Hosting;

namespace AustraliaWeatherPrediction.Api.Extensions;

public static class HostEnvironmentExtensions
{
    public static bool IsDebug(this IHostEnvironment environment) 
        => environment.IsEnvironment("Debug");
}