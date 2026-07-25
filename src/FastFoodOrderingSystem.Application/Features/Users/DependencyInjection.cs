using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;
using FastFoodOrderingSystem.Application.Features.Users.GetUserProfile;
using FastFoodOrderingSystem.Application.Features.Users.GetCurrentUserProfile;
using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Users;

internal static class DependencyInjection
{
    public static IServiceCollection AddUserHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetUserProfileHandler>();
        services.AddScoped(sp =>
        {
            IHandler<GetUserProfileQuery, PublicUserProfileResponse> handler = sp.GetRequiredService<GetUserProfileHandler>();

            handler = new CachingQueryDecorator<GetUserProfileQuery, PublicUserProfileResponse>(
                handler,
                sp.GetRequiredService<ICacheStore<PublicUserProfileResponse>>(),
                sp.GetRequiredService<ILogger<CachingQueryDecorator<GetUserProfileQuery, PublicUserProfileResponse>>>(),
                sp.GetRequiredService<ICachePolicy<GetUserProfileQuery>>());

            handler = new PerformanceHandlerDecorator<GetUserProfileQuery, PublicUserProfileResponse>(
                handler,
                sp.GetRequiredService<
                    ILogger<PerformanceHandlerDecorator<GetUserProfileQuery, PublicUserProfileResponse>>>());

            handler = new LoggingHandlerDecorator<GetUserProfileQuery, PublicUserProfileResponse>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<GetUserProfileQuery, PublicUserProfileResponse>>>());
            
            return handler;
        });
        
        services.AddScoped<GetCurrentUserProfileHandler>();
        services.AddScoped(sp =>
        {
            IHandler<GetCurrentUserProfileQuery, PrivateUserProfileResponse> handler = sp.GetRequiredService<GetCurrentUserProfileHandler>();

            handler = new CachingQueryDecorator<GetCurrentUserProfileQuery, PrivateUserProfileResponse>(
                handler,
                sp.GetRequiredService<ICacheStore<PrivateUserProfileResponse>>(),
                sp.GetRequiredService<ILogger<CachingQueryDecorator<GetCurrentUserProfileQuery, PrivateUserProfileResponse>>>(),
                sp.GetRequiredService<ICachePolicy<GetCurrentUserProfileQuery>>());

            handler = new PerformanceHandlerDecorator<GetCurrentUserProfileQuery, PrivateUserProfileResponse>(
                handler,
                sp.GetRequiredService<
                    ILogger<PerformanceHandlerDecorator<GetCurrentUserProfileQuery, PrivateUserProfileResponse>>>());

            handler = new LoggingHandlerDecorator<GetCurrentUserProfileQuery, PrivateUserProfileResponse>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<GetCurrentUserProfileQuery, PrivateUserProfileResponse>>>());
            
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