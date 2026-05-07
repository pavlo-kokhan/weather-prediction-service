using AustraliaWeatherPrediction.Api.Endpoints;
using AustraliaWeatherPrediction.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AustraliaWeatherPrediction.Api.Extensions;

public static class EndpointRouterBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints
            .MapGroup("/api")
            .AddEndpointFilter<ValidationFilter>();

        apiGroup.MapWeatherPredictionEndpoints();

        return endpoints;
    }
}