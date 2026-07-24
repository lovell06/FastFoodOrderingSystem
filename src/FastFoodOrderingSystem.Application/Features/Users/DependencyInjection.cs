using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;
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
                sp.GetRequiredService<ICacheStore<UserProfileResponse>>(),
                sp.GetRequiredService<ILogger<CachingQueryDecorator<GetProfileQuery, UserProfileResponse>>>(),
                sp.GetRequiredService<ICachePolicy<GetProfileQuery>>());

            handler = new PerformanceHandlerDecorator<GetProfileQuery, UserProfileResponse>(
                handler,
                sp.GetRequiredService<
                    ILogger<PerformanceHandlerDecorator<GetProfileQuery, UserProfileResponse>>>());

            handler = new LoggingHandlerDecorator<GetProfileQuery, UserProfileResponse>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<GetProfileQuery, UserProfileResponse>>>());
            
            return handler;
        });
        
        services.AddScoped<UpdateProfileHandler>();
        services.AddScoped(sp =>
        {
            IHandler<UpdateProfileCommand, Unit> handler = sp.GetRequiredService<UpdateProfileHandler>();

            handler = new TransactionCommandDecorator<UpdateProfileCommand, Unit>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<UpdateProfileCommand, Unit>>>());

            handler = new PerformanceHandlerDecorator<UpdateProfileCommand, Unit>(
                handler,
                sp.GetRequiredService<
                    ILogger<PerformanceHandlerDecorator<UpdateProfileCommand, Unit>>>());

            handler = new LoggingHandlerDecorator<UpdateProfileCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<UpdateProfileCommand, Unit>>>());
            
            return handler;
        });
        return services;
    }
}