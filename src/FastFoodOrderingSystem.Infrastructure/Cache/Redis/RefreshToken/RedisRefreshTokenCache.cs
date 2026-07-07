using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.RefreshToken;

public sealed class RedisRefreshTokenCache : IRefreshTokenStore
{
    private readonly IDatabase _database;
    private readonly RedisKeyProvider _keyProvider;
    private readonly JsonSerializerOptions _options;
    public RedisRefreshTokenCache(IConnectionMultiplexer connectionMultiplexer, RedisKeyProvider keyProvider, JsonSerializerOptions options)
    {
        _database = connectionMultiplexer.GetDatabase();
        _keyProvider = keyProvider;
        _options = options;
    }
    public async Task<Domain.Common.ValueObjects.RefreshToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var key = _keyProvider.RefreshToken(userId);

        var json = await _database.StringGetAsync(key);

        if (!json.HasValue)
            return null;
        
        var snapshot = JsonSerializer.Deserialize<RefreshTokenSnapshot>(
            (string)json!,
            _options);

        var token = RefreshTokenMapper.ToValueObject(snapshot!);

        return token;

    }

    public async Task<bool> RemoveByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var key = _keyProvider.RefreshToken(userId);

        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> SaveAsync(Domain.Common.ValueObjects.RefreshToken token, IDateTimeProvider clock, CancellationToken cancellationToken)
    {
        var key = _keyProvider.RefreshToken(token.UserId);
        var snapshot = new RefreshTokenSnapshot(
            UserId: token.UserId, 
            Token: token.Token, 
            ExpiresAt: token.ExpiresAt);

        var ttl = token.ExpiresAt - clock.UtcNow;
        if (ttl < TimeSpan.Zero)
            return false;

        var json = JsonSerializer.Serialize(value: snapshot, options: _options);
        return await _database.StringSetAsync(
            key: key,
            value: json,
            expiry: ttl);
    }
}