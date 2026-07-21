using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventHandlers.Customers;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEvents.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventDispatchers;

public static class DependencyInjection
{
    public static IServiceCollection AddIntegrationEventDispatchers(this IServiceCollection services)
    {
        services.AddScoped<IEventDispatcher, EventDispatcher>();
        return services;
    }
}