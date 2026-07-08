using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IOtpConfiguration, OtpConfiguration>();
        services.AddScoped<IEmailConfiguration, GmailConfiguration>();
        services.AddScoped<IRefreshTokenConfiguration, RefreshTokenConfiguration>();
        services.AddScoped<IAccessTokenConfiguration, AccessTokenConfiguration>();
        return services;
    }
}