using FastFoodOrderingSystem.Application.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Time;

public static class DependencyInjection
{
    public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}