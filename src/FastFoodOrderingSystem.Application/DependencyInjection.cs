using FastFoodOrderingSystem.Application.Features.Auth;
using FastFoodOrderingSystem.Application.Features.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAuthenticationHandlers();
        services.AddCustomerHandlers();
        return services;
    }
}