namespace AustraliaWeatherPrediction.Api.Application.Requests;

public record MaxTemperaturePredictionRequest(
    float MinTemp,
    float Rainfall,
    float WindGustSpeed,
    float Humidity9am,
    float Humidity3pm,
    float Pressure9am,
    float Pressure3pm,
    int RainToday)
{
    public float[] ToFeatureArray() 
        => [MinTemp, Rainfall, WindGustSpeed, Humidity9am, Humidity3pm, Pressure9am, Pressure3pm, RainToday];
}