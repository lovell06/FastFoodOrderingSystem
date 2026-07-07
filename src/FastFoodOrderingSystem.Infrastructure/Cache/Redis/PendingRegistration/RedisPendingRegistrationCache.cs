using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.PendingRegistration;

public class RedisPendingRegistrationCache : IPendingRegistrationStore
{
    private readonly IDatabase _database;
    private readonly RedisKeyProvider _redisKeyProvider;
    private readonly JsonSerializerOptions _options;

    public RedisPendingRegistrationCache(
        IConnectionMultiplexer connectionMultiplexer,
        RedisKeyProvider redisKeyProvider,
        JsonSerializerOptions options)
    {
        _database = connectionMultiplexer.GetDatabase();
        _redisKeyProvider = redisKeyProvider;
        _options = options;
    }

    public async Task<Domain.Users.PendingRegistration?> GetByEmailAsync(Email email,
        CancellationToken cancellationToken = default)
    {
        var key = _redisKeyProvider.PendingRegistration(email);
        var json = await _database.StringGetAsync(key);

        if (!json.HasValue)
            return null;

        var snapshot = JsonSerializer.Deserialize<PendingRegistrationSnapshot>(
            json: json!,
            options: _options);

        if (snapshot is null)
            throw new InvalidOperationException("Cannot Deserialize PendingRegistration.");

        var pending = PendingRegistrationMapper.ToEntity(snapshot);
        return pending;
    }

    public async Task<bool> RemoveAsync(Email email, CancellationToken cancellationToken = default)
    {
        var key = _redisKeyProvider.PendingRegistration(email);
        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> SaveAsync(
        Domain.Users.PendingRegistration pendingRegistration,
        IDateTimeProvider clock,
        CancellationToken cancellationToken = default)
    {
        var key = _redisKeyProvider.PendingRegistration(pendingRegistration.Id);
        var json = JsonSerializer.Serialize(
            value: pendingRegistration,
            options: _options);
        
        var ttl = pendingRegistration.ExpiresAt - clock.UtcNow;
        if (ttl < TimeSpan.Zero)
            return false;

        return await _database.StringSetAsync(
            key: key,
            value: json,
            expiry: ttl);
    }
}