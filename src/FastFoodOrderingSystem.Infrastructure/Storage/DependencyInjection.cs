using FastFoodOrderingSystem.Application.Abstractions.storage;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Storage;

internal static class DependencyInjection
{
    public static IServiceCollection AddStorageServices(this IServiceCollection services)
    {
        services.AddScoped<IFileStorage, LocalFileStorage>();
        
        return services;
    }
}