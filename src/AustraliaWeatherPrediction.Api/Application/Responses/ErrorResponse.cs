using System;

namespace AustraliaWeatherPrediction.Api.Application.Responses;

public record ErrorResponse(
    string Message,
    Exception? Exception,
    int StatusCode);