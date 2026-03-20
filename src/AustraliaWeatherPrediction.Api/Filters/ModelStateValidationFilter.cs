using System.Linq;
using System.Threading.Tasks;
using AustraliaWeatherPrediction.Api.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AustraliaWeatherPrediction.Api.Filters;

public class ModelStateValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorMessages = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            var combinedMessage = $"Validation failed: {string.Join("; ", errorMessages)}";
            
            var errorResponse = new ErrorResponse(
                Message: combinedMessage,
                Exception: null,
                StatusCode: StatusCodes.Status400BadRequest
            );

            context.Result = new BadRequestObjectResult(errorResponse);
            
            return;
        }

        await next();
    }
}