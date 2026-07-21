using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventHandlers.Customers;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEvents.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventHandlers;

public static class DependencyInjection
{
    public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IEventHandler<IntegrationUserRegisteredEvent>, SendWelcomeEmailHandler>();
        return services;
    }
}