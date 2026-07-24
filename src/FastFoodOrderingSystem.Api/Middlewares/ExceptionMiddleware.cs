using FastFoodOrderingSystem.Application.Common.Errors;

namespace FastFoodOrderingSystem.Api.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, SystemError.Unexpected.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                SystemError.Unexpected.Code,
                SystemError.Unexpected.Message
            });
        }
    }
}