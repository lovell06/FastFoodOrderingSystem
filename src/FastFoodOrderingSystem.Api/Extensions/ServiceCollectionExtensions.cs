using FastFoodOrderingSystem.Api.Configurations;
using FastFoodOrderingSystem.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(config =>
        {
            config.LowercaseUrls = true;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        
        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerConfigureOptions>();

        services.AddAuthorization();
        
        services.AddControllers();

        services.AddScoped<ExceptionMiddleware>();
        
        return services;
    }
}