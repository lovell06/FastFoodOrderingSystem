using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.PendingRegistration;

public class RedisPendingRegistrationCache : IPendingRegistrationStore
{
    private readonly IDatabase _database;
    private readonly RedisKeyProvider _redisKeyProvider;

    public RedisPendingRegistrationCache(
        IConnectionMultiplexer connectionMultiplexer,
        RedisKeyProvider redisKeyProvider)
    {
        _database = connectionMultiplexer.GetDatabase();
        _redisKeyProvider = redisKeyProvider;
    }

    public async Task<Domain.Users.PendingRegistration?> GetAsync(Email email,
        CancellationToken cancellationToken = default)
    {
        var key = _redisKeyProvider.PendingRegistration(email);
        var json = await _database.StringGetAsync(key);

        if (!json.HasValue)
            return null;

        var pending = JsonSerializer.Deserialize<Domain.Users.PendingRegistration>((string)json!);
        return pending ??
               throw new InvalidOperationException("Cannot Deserialize PendingRegistration.");
    }

    public async Task<bool> RemoveAsync(Email email, CancellationToken cancellationToken = default)
    {
        var key = _redisKeyProvider.PendingRegistration(email);
        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> SaveAsync(Domain.Users.PendingRegistration pendingRegistration,
        CancellationToken cancellationToken = default)
    {
        var key = _redisKeyProvider.PendingRegistration(pendingRegistration.Id);
        var value = JsonSerializer.Serialize(pendingRegistration);
        var ttl = pendingRegistration.ExpiresAt - DateTime.Now;

        return await _database.StringSetAsync(
            key,
            value,
            ttl);
    }
}