using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AustraliaWeatherPrediction.Api.Filters;

public class ValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var serviceProvider = context.HttpContext.RequestServices;

        foreach (var argument in context.Arguments)
        {
            if (argument is null) 
                continue;

            var argumentType = argument.GetType();

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);

            if (serviceProvider.GetService(validatorType) is IValidator validator)
            {
                var validationContext = new ValidationContext<object>(argument);
                var validationResult = await validator.ValidateAsync(validationContext);

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
            }
        }

        return await next(context);
    }
}