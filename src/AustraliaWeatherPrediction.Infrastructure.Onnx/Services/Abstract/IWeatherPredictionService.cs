namespace AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;

public interface IWeatherPredictionService
{
    Task InitializeAsync();
    
    ValueTask<bool> PredictRainAsync(float[] features, CancellationToken cancellationToken = default);
    
    ValueTask<float> PredictMaxTemperatureAsync(float[] features, CancellationToken cancellationToken = default);
}