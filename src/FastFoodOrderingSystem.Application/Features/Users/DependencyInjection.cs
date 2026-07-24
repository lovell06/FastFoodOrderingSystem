using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;
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
            IHandler<GetProfileQuery, UserProfileResponse> handler = sp.GetRequiredService<GetProfileHandler>();

            handler = new CachingQueryDecorator<GetProfileQuery, UserProfileResponse>(
                handler,
                sp.GetRequiredService<ICacheStore<GetProfileQuery, UserProfileResponse>>(),
                sp.GetRequiredService<ILogger<CachingQueryDecorator<GetProfileQuery, UserProfileResponse>>>());

            handler = new PerformanceHandlerDecorator<GetProfileQuery, UserProfileResponse>(
                handler,
                sp.GetRequiredService<
                    ILogger<PerformanceHandlerDecorator<GetProfileQuery, UserProfileResponse>>>());

            handler = new LoggingHandlerDecorator<GetProfileQuery, UserProfileResponse>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<GetProfileQuery, UserProfileResponse>>>());
            
            return handler;
        });
        return services;
    }
}