using System.Threading;
using System.Threading.Tasks;
using AustraliaWeatherPrediction.Contracts.Requests;
using AustraliaWeatherPrediction.Contracts.Responses;
using AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AustraliaWeatherPrediction.Api.Endpoints;

public static class WeatherPredictionEndpoints
{
    public static IEndpointRouteBuilder MapWeatherPredictionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("weather-predictions/rain", GetRainPredictionAsync);
        endpoints.MapPost("weather-predictions/max-temperature", GetMaxTemperaturePredictionAsync);
        
        return endpoints;
    }

    private static async Task<IResult> GetRainPredictionAsync(
        [FromBody] RainPredictionRequest request,
        [FromServices] IWeatherPredictionService weatherPredictionService,
        CancellationToken cancellationToken)
    {
        var prediction = await weatherPredictionService
            .PredictRainAsync(request.ToFeatureArray(), cancellationToken);

        var response = new RainPredictionResponse(
            prediction,
            prediction ? "Yes" : "No",
            $"Model predicted that tomorrow will {(prediction ? "" : "not")} be rainy");

        return Results.Ok(response);
    }

    private static async Task<IResult> GetMaxTemperaturePredictionAsync(
        [FromBody] MaxTemperaturePredictionRequest request,
        [FromServices] IWeatherPredictionService weatherPredictionService,
        CancellationToken cancellationToken)
    {
        var maxTemperature = await weatherPredictionService
            .PredictMaxTemperatureAsync(request.ToFeatureArray(), cancellationToken);

        var response = new MaxTemperaturePredictionResponse(
            maxTemperature,
            $"Model predicted that max temperature will be {maxTemperature:F2} °C");

        return Results.Ok(response);
    }
}