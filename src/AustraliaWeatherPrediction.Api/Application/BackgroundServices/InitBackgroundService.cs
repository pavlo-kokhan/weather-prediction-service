using System;
using System.Threading;
using System.Threading.Tasks;
using AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AustraliaWeatherPrediction.Api.Application.BackgroundServices;

public class InitBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public InitBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeWeatherPredictionServiceAsync();
    }
    
    private async Task InitializeWeatherPredictionServiceAsync()
    {
        var service = _serviceProvider.GetRequiredService<IWeatherPredictionService>();
        
        await service.InitializeAsync();
    }
}