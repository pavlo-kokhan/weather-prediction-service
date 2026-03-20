namespace AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;

public interface IWeatherPredictionService
{
    Task InitializeAsync();
    
    ValueTask<bool> PredictRain(float[] features, CancellationToken cancellationToken = default);
    
    ValueTask<float> PredictMaxTemperature(float[] features, CancellationToken cancellationToken = default);
}