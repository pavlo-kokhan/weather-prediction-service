using FluentValidation;

namespace AustraliaWeatherPrediction.Api.Application.Validators;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, float> MustBeValidTemperature<T>(this IRuleBuilder<T, float> ruleBuilder) 
        => ruleBuilder
            .InclusiveBetween(-30f, 60f)
            .WithMessage("{PropertyName} must be between -30 and 60 °C.");

    public static IRuleBuilderOptions<T, float> MustBeValidRainfall<T>(this IRuleBuilder<T, float> ruleBuilder) 
        => ruleBuilder
            .GreaterThanOrEqualTo(0f)
            .WithMessage("{PropertyName} cannot be negative.");

    public static IRuleBuilderOptions<T, float> MustBeValidWindSpeed<T>(this IRuleBuilder<T, float> ruleBuilder) 
        => ruleBuilder
            .GreaterThanOrEqualTo(0f).WithMessage("{PropertyName} cannot be negative.")
            .LessThan(200f).WithMessage("The specified {PropertyName} seems unrealistic (>200 km/h).");

    public static IRuleBuilderOptions<T, float> MustBeValidHumidity<T>(this IRuleBuilder<T, float> ruleBuilder) 
        => ruleBuilder
            .InclusiveBetween(0f, 100f)
            .WithMessage("{PropertyName} must be a percentage (0-100).");

    public static IRuleBuilderOptions<T, float> MustBeValidPressure<T>(this IRuleBuilder<T, float> ruleBuilder) 
        => ruleBuilder
            .InclusiveBetween(900f, 1100f)
            .WithMessage("{PropertyName} must be between 900 and 1100 hPa.");

    public static IRuleBuilderOptions<T, int> MustBeBinary<T>(this IRuleBuilder<T, int> ruleBuilder) 
        => ruleBuilder
            .Must(x => x is 0 or 1)
            .WithMessage("{PropertyName} must be binary (0 or 1).");
}