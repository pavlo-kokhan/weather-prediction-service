namespace AustraliaWeatherPrediction.Api.Application.Requests;

public record RainPredictionRequest(
    float MinTemp,
    float MaxTemp,
    float Rainfall,
    float WindGustSpeed,
    float Humidity9am,
    float Humidity3pm,
    float Pressure9am,
    float Pressure3pm,
    int RainToday)
{
    public float[] ToFeatureArray() 
        => [MinTemp, MaxTemp, Rainfall, WindGustSpeed, Humidity9am, Humidity3pm, Pressure9am, Pressure3pm, RainToday];
}