using System;
using System.IO;
using AustraliaWeatherPrediction.Api.Application.BackgroundServices;
using AustraliaWeatherPrediction.Api.Extensions;
using AustraliaWeatherPrediction.Api.Middlewares;
using AustraliaWeatherPrediction.Infrastructure.Onnx.Services;
using AustraliaWeatherPrediction.Infrastructure.Onnx.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddOpenApi()
    .AddCors()
    .AddEndpointsApiExplorer()
    .AddFluentValidation()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddHostedService<InitBackgroundService>()
    .AddSingleton<IWeatherPredictionService>(_ =>
    {
        var baseDirectory = AppContext.BaseDirectory;
        var classifierModelPath = Path.Combine(baseDirectory, "Resources/rf_classifier.onnx");
        var regressorModelPath = Path.Combine(baseDirectory, "Resources/rf_regressor.onnx");
        
        return new OnnxWeatherPredictionService(classifierModelPath, regressorModelPath);
    });

var app = builder.Build();

app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());

if (app.Environment.IsDebug() || app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options
            .WithTitle("Australia Weather Prediction API")
            .WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseExceptionHandler();
app.MapEndpoints();

await app.RunAsync();