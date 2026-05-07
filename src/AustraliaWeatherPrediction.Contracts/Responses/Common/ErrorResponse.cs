namespace AustraliaWeatherPrediction.Contracts.Responses.Common;

public record ErrorResponse(
    string Message,
    Exception? Exception,
    int StatusCode);