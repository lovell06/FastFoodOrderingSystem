using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Users;

internal static class DependencyInjection
{
    public static IServiceCollection AddUserHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetProfileHandler>();
        services.AddScoped(sp =>
        {
            IHandler<GetProfileQuery, GetProfileResponse> handler = sp.GetRequiredService<GetProfileHandler>();

            handler = new PerformanceHandlerDecorator<GetProfileQuery, GetProfileResponse>(
                handler,
                sp.GetRequiredService<
                    ILogger<PerformanceHandlerDecorator<GetProfileQuery, GetProfileResponse>>>());

            handler = new LoggingHandlerDecorator<GetProfileQuery, GetProfileResponse>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<GetProfileQuery, GetProfileResponse>>>());
            
            return handler;
        });
        return services;
    }
}