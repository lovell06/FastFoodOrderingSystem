using FastFoodOrderingSystem.Api.Middlewares;

namespace FastFoodOrderingSystem.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddAuthentication();

        services.AddAuthorization();
        
        services.AddControllers();

        services.AddScoped<ExceptionMiddleware>();
        
        return services;
    }
}