using FastFoodOrderingSystem.Infrastructure.Authentication;
using FastFoodOrderingSystem.Infrastructure.Cache;
using FastFoodOrderingSystem.Infrastructure.Configurations;
using FastFoodOrderingSystem.Infrastructure.Emails;
using FastFoodOrderingSystem.Infrastructure.Eventing;
using FastFoodOrderingSystem.Infrastructure.Mediator;
using FastFoodOrderingSystem.Infrastructure.Options;
using FastFoodOrderingSystem.Infrastructure.Persistence;
using FastFoodOrderingSystem.Infrastructure.Storage;
using FastFoodOrderingSystem.Infrastructure.Time;
using FastFoodOrderingSystem.Infrastructure.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureOptions(configuration);

        services.AddConfigurations();

        services.AddDateTimeProvider();

        services.AddPersistence(configuration);

        services.AddCacheService();

        services.AddEmailServices();

        services.AddAuthenticationServices();

        services.AddStorageServices();

        services.AddEventsDispatcher();

        services.AddWorkers();

        services.AddMediator();
        
        return services;
    }
}