using FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;
using FastFoodOrderingSystem.Application.Abstractions.Cache.PendingRegistration;
using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.ForgotPasswordOtp;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.PendingRegistration;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.RefreshToken;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache;

public static class DependencyInjection
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
        return services;
    }
}