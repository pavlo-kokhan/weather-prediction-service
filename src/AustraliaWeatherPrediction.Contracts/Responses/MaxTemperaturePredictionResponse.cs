namespace AustraliaWeatherPrediction.Contracts.Responses;

public record MaxTemperaturePredictionResponse(
    float MaxTemperature,
    string Message);