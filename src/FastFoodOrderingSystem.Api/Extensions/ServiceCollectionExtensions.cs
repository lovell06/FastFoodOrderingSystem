using FastFoodOrderingSystem.Api.Middlewares;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodOrderingSystem.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        
        services.Configure<RouteOptions>(config =>
        {
            config.LowercaseUrls = true;
        });

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer((options) =>
            {
                var jwtOptions = provider.GetRequiredService<JwtOption>();

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.Key))
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = provider.GetRequiredService<ILogger>();
                        logger.LogWarning(context.Exception, "JWT validation failed.");
                        return Task.CompletedTask;
                    },

                    OnForbidden = async context =>
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Message = "Permission denied."
                        });
                    }
                };
            });

        services.AddAuthorization();
        
        services.AddControllers();

        services.AddScoped<ExceptionMiddleware>();
        
        return services;
    }
}