namespace AustraliaWeatherPrediction.Api.Application.Responses;

public record RainPredictionResponse(
    bool Prediction,
    string Result,
    string Message);