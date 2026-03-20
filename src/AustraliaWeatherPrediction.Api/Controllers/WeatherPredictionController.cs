using System.Threading;
using System.Threading.Tasks;
using AustraliaWeatherPrediction.Api.Application.Requests;
using AustraliaWeatherPrediction.Api.Application.Responses;
using AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AustraliaWeatherPrediction.Api.Controllers;

[ApiController]
[Route("weather-predictions")]
public class WeatherPredictionController : ControllerBase
{
    private readonly IWeatherPredictionService _weatherPredictionService;

    public WeatherPredictionController(IWeatherPredictionService weatherPredictionService)
    {
        _weatherPredictionService = weatherPredictionService;
    }

    [HttpPost("rain")]
    public async Task<IActionResult> GetRainPredictionAsync([FromBody] RainPredictionRequest request, CancellationToken cancellationToken)
    {
        var prediction = await _weatherPredictionService.PredictRainAsync(request.ToFeatureArray(), cancellationToken);

        var response = new RainPredictionResponse(
            prediction,
            prediction ? "Yes" : "No",
            $"Model predicted that tomorrow will {(prediction ? "" : "not")} be rainy");
        
        return Ok(response);
    }
    
    [HttpPost("max-temperature")]
    public async Task<IActionResult> GetMaxTemperaturePredictionAsync([FromBody] MaxTemperaturePredictionRequest request, CancellationToken cancellationToken)
    {
        var maxTemperature = await _weatherPredictionService.PredictMaxTemperatureAsync(request.ToFeatureArray(), cancellationToken);

        var response = new MaxTemperaturePredictionResponse(
            maxTemperature,
            $"Model predicted that max temperature will be {maxTemperature:F2} °C");
        
        return Ok(response);
    }
}