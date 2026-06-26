using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // services.AddDbContext<ApplicationDbContext>();
        return services;
    }
}