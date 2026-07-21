using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventDispatchers;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Eventing;

internal static class DependencyInjection
{
    public static IServiceCollection AddEventsDispatcher(this IServiceCollection services)
    {
        services.AddIntegrationEventHandlers();
        services.AddIntegrationEventDispatchers();
        return services;
    }
}