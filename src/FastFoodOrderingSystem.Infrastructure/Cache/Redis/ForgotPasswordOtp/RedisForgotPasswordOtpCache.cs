using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.ForgotPasswordOtp;

public class RedisForgotPasswordOtpCache : IForgotPasswordOtpStore
{
    private readonly RedisKeyProvider _keyProvider;
    private readonly IDatabase _database;

    public RedisForgotPasswordOtpCache(
        RedisKeyProvider keyProvider,
        IConnectionMultiplexer connectionMultiplexer)
    {
        _keyProvider = keyProvider;
        _database = connectionMultiplexer.GetDatabase();
    }

    public async Task<Application.Abstractions.Cache.ForgotPasswordOtp.ForgotPasswordOtp?> GetByEmailAsync(Email email,
        CancellationToken cancellationToken = default)
    {
        var key = _keyProvider.ForgotPasswordOtp(email);

        var json = await _database.StringGetAsync(key);

        if (!json.HasValue)
            return null;

        var snapshot = JsonSerializer.Deserialize<ForgotPasswordOtpSnapshot>(json: json!);

        if (snapshot is null)
            throw new InvalidOperationException("Cannot deserialize json -> ForgotPasswordOtpSnapshot.");
        
        return ForgotPasswordOtpMapper.ToEntity(snapshot);
    }

    public async Task<bool> RemoveByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var key = _keyProvider.ForgotPasswordOtp(email);

        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> SaveAsync(Application.Abstractions.Cache.ForgotPasswordOtp.ForgotPasswordOtp forgotPasswordOtp,
        IDateTimeProvider clock, CancellationToken cancellationToken = default)
    {
        var ttl = forgotPasswordOtp.ExpiresAt - clock.UtcNow;
        if (ttl < TimeSpan.Zero)
            return false;
        
        var snapshot = ForgotPasswordOtpMapper.ToSnapshot(forgotPasswordOtp);

        var key = _keyProvider.ForgotPasswordOtp(forgotPasswordOtp.Id);

        var json = JsonSerializer.Serialize(snapshot);
        
        return await _database.StringSetAsync(
            key: key,
            value: json,
            expiry: ttl);
    }
}