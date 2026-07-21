using FastFoodOrderingSystem.Application.Features.Auth;
using FastFoodOrderingSystem.Application.Features.Customers;
using FastFoodOrderingSystem.Application.Features.Users;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAuthenticationHandlers();
        services.AddCustomerHandlers();
        services.AddUserHandlers();
        return services;
    }
}