using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;
using FastFoodOrderingSystem.Application.Abstractions.Cache.PendingRegistration;
using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.ForgotPasswordOtp;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.PendingRegistration;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.RefreshToken;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.UserProfile;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.UserProfile.Policies;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache;

internal static class DependencyInjection
{
    public static IServiceCollection AddCacheService(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var option = sp.GetRequiredService<IOptions<RedisOption>>().Value;
            return ConnectionMultiplexer.Connect(option.ConnectionStrings);
        });
        services.AddScoped<RedisKeyProvider>();
        services.AddScoped<IPendingRegistrationStore, RedisPendingRegistrationCache>();
        services.AddScoped<IRefreshTokenStore, RedisRefreshTokenCache>();
        services.AddScoped<IForgotPasswordOtpStore, RedisForgotPasswordOtpCache>();

        services.AddScoped<ICacheStore<UserProfileResponse>, RedisUserProfileCache>();
        
        // Register policies
        services.AddScoped<ICachePolicy<GetProfileQuery>, GetProfileQueryPolicy>();
        services.AddScoped<ICachePolicy<UpdateProfileCommand>, UpdateProfileCommandPolicy>();
        
        return services;
    }
}