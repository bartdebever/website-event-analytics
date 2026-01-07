using BartdeBever.EventTracking.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BartdeBever.EventTracking.Filters;

public class ApiKeyAuthenticationFilter : IAsyncActionFilter
{
    private const string ApiKeyHeaderName = "X-API-Key";
    private readonly ApplicationRepository _applicationRepository;

    public ApiKeyAuthenticationFilter(ApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyValue))
        {
            context.Result = new UnauthorizedObjectResult(new { error = "API Key is missing" });
            return;
        }

        var apiKey = apiKeyValue.ToString();
        var application = await _applicationRepository.GetByApiKeyAsync(apiKey);

        if (application == null)
        {
            context.Result = new UnauthorizedObjectResult(new { error = "Invalid API Key" });
            return;
        }

        // Store the application in HttpContext items for later use
        context.HttpContext.Items["Application"] = application;

        await next();
    }
}
