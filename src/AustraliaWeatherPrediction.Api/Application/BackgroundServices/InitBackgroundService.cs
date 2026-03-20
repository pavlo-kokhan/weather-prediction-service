using System;
using System.Threading;
using System.Threading.Tasks;
using AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AustraliaWeatherPrediction.Api.Application.BackgroundServices;

public class InitBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<InitBackgroundService> _logger;

    public InitBackgroundService(IServiceProvider serviceProvider, ILogger<InitBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Initializing WeatherPredictionService");
        
        await InitializeWeatherPredictionServiceAsync();
        
        _logger.LogInformation("WeatherPredictionService is initialized");
    }
    
    private async Task InitializeWeatherPredictionServiceAsync()
    {
        var service = _serviceProvider.GetRequiredService<IWeatherPredictionService>();
        
        await service.InitializeAsync();
    }
}