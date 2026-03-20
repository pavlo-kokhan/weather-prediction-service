using System;
using System.IO;
using System.Text.Json.Serialization;
using AustraliaWeatherPrediction.Api.Application.BackgroundServices;
using AustraliaWeatherPrediction.Api.Extensions;
using AustraliaWeatherPrediction.Api.Filters;
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
    })
    .AddControllers(options =>
    {
        options.Filters.Add<ModelStateValidationFilter>();
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
app.MapControllers();

await app.RunAsync();