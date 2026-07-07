using FastFoodOrderingSystem.Application.Features.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAuthenticationHandlers();
        return services;
    }
}