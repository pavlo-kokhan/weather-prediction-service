namespace AustraliaWeatherPrediction.Api.Application.Responses;

public record MaxTemperaturePredictionResponse(
    float MaxTemperature,
    string Message);