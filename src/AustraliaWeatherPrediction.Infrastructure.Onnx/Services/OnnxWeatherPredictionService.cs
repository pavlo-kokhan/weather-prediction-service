using AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace AustraliaWeatherPrediction.Infrastructure.Onnx.Services;

public class OnnxWeatherPredictionService : IWeatherPredictionService, IDisposable
{
    private readonly string _classifierModelPath;
    private readonly string _regressorModelPath;
    
    private InferenceSession? _classifierSession;
    private InferenceSession? _regressorSession;
    private bool _isInitialized;

    public OnnxWeatherPredictionService(string classifierModelPath, string regressorModelPath)
    {
        _classifierModelPath = classifierModelPath;
        _regressorModelPath = regressorModelPath;
    }
    
    public async Task InitializeAsync()
    {
        var classifierInitializationTask = Task.Run(() =>
        {
            _classifierSession = new InferenceSession(_classifierModelPath);
        });

        var regressionInitializationTask = Task.Run(() =>
        {
            _regressorSession = new InferenceSession(_regressorModelPath);
        });
        
        await Task.WhenAll(classifierInitializationTask, regressionInitializationTask);
        
        _isInitialized = true;
    }

    public ValueTask<bool> PredictRain(float[] features, CancellationToken cancellationToken = default)
    {
        if (!_isInitialized || _classifierSession is null)
            throw new InvalidOperationException("Model is not loaded");
        
        var tensor = new DenseTensor<float>(features, new[] { 1, features.Length });
        
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("float_input", tensor) 
        };

        using var results = _classifierSession.Run(inputs);
        
        var prediction = results
            .First()
            .AsEnumerable<long>()
            .First();
        
        return ValueTask.FromResult(prediction == 1);
    }

    public ValueTask<float> PredictMaxTemperature(float[] features, CancellationToken cancellationToken = default)
    {
        if (!_isInitialized || _regressorSession is null)
            throw new InvalidOperationException("Model is not loaded");
        
        var tensor = new DenseTensor<float>(features, new[] { 1, features.Length });
        
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("float_input", tensor)
        };

        using var results = _regressorSession.Run(inputs);
        
        var prediction = results
            .First()
            .AsEnumerable<float>()
            .First();
        
        return ValueTask.FromResult(prediction);
    }

    public void Dispose()
    {
        _classifierSession?.Dispose();
        _regressorSession?.Dispose();
    }
}