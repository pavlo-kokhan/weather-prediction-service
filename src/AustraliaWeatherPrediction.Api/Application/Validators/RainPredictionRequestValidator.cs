using AustraliaWeatherPrediction.Api.Application.Requests;
using FluentValidation;

namespace AustraliaWeatherPrediction.Api.Application.Validators;

public class RainPredictionRequestValidator : AbstractValidator<RainPredictionRequest>
{
    public RainPredictionRequestValidator()
    {
        RuleFor(x => x.MinTemp).MustBeValidTemperature();
        
        RuleFor(x => x.MaxTemp)
            .MustBeValidTemperature()
            .GreaterThanOrEqualTo(x => x.MinTemp)
            .WithMessage("{PropertyName} cannot be less than the minimum temperature.");

        RuleFor(x => x.Rainfall).MustBeValidRainfall();
        RuleFor(x => x.WindGustSpeed).MustBeValidWindSpeed();
        RuleFor(x => x.Humidity9am).MustBeValidHumidity();
        RuleFor(x => x.Humidity3pm).MustBeValidHumidity();
        RuleFor(x => x.Pressure9am).MustBeValidPressure();
        RuleFor(x => x.Pressure3pm).MustBeValidPressure();
        RuleFor(x => x.RainToday).MustBeBinary();
    }
}