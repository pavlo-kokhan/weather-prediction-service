using AustraliaWeatherPrediction.Api.Application.Requests;
using FluentValidation;

namespace AustraliaWeatherPrediction.Api.Application.Validators;

public class MaxTemperaturePredictionRequestValidator : AbstractValidator<MaxTemperaturePredictionRequest>
{
    public MaxTemperaturePredictionRequestValidator()
    {
        RuleFor(x => x.MinTemp).MustBeValidTemperature();
        RuleFor(x => x.Rainfall).MustBeValidRainfall();
        RuleFor(x => x.WindGustSpeed).MustBeValidWindSpeed();
        RuleFor(x => x.Humidity9am).MustBeValidHumidity();
        RuleFor(x => x.Humidity3pm).MustBeValidHumidity();
        RuleFor(x => x.Pressure9am).MustBeValidPressure();
        RuleFor(x => x.Pressure3pm).MustBeValidPressure();
        RuleFor(x => x.RainToday).MustBeBinary();
    }
}