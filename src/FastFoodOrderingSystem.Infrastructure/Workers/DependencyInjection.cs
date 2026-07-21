using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Workers;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkers(this IServiceCollection services)
    {
        services.AddHostedService<OutboxWorker>();
        services.AddHostedService<OutboxCleanupWorker>();
        return services;
    }
}