namespace AustraliaWeatherPrediction.Contracts.Responses;

public record RainPredictionResponse(
    bool Prediction,
    string Result,
    string Message);