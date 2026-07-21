using FastFoodOrderingSystem.Application.Abstractions.Emails;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Emails;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, GmailSender>();
        return services;
    }
}