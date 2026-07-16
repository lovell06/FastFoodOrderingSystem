using FastFoodOrderingSystem.Application.Abstractions.storage;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Storage;

public static class DependencyInjection
{
    public static IServiceCollection AddStorageServices(this IServiceCollection services)
    {
        services.AddScoped<IFileStorage, LocalFileStorage>();
        
        return services;
    }
}