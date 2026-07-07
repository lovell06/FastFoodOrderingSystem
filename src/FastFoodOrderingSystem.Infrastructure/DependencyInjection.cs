using FastFoodOrderingSystem.Infrastructure.Authentication;
using FastFoodOrderingSystem.Infrastructure.Cache;
using FastFoodOrderingSystem.Infrastructure.Configurations;
using FastFoodOrderingSystem.Infrastructure.Emails;
using FastFoodOrderingSystem.Infrastructure.Options;
using FastFoodOrderingSystem.Infrastructure.Persistence;
using FastFoodOrderingSystem.Infrastructure.Serialization;
using FastFoodOrderingSystem.Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureOptions(configuration);

        services.AddConfigurations();

        services.AddSerialization();

        services.AddDateTimeProvider();

        services.AddPersistence(configuration);

        services.AddCacheService();

        services.AddEmailServices();

        services.AddAuthenticationServices();
        
        return services;
    }
}