using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.RefreshTokens;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.RefreshToken;

public sealed class RedisRefreshTokenCache : IRefreshTokenStore
{
    private readonly IDatabase _database;
    private readonly RedisKeyProvider _keyProvider;
    public RedisRefreshTokenCache(
        IConnectionMultiplexer connectionMultiplexer, 
        RedisKeyProvider keyProvider)
    {
        _database = connectionMultiplexer.GetDatabase();
        _keyProvider = keyProvider;
    }

    public async Task<Domain.RefreshTokens.RefreshToken?> GetByIdAsync(TokenId id, CancellationToken cancellationToken)
    {
        var key = _keyProvider.RefreshToken(id);

        var json = await _database.StringGetAsync(key);

        if (!json.HasValue)
            return null;
            
        var snapshot = JsonSerializer.Deserialize<RefreshTokenSnapshot>(json: json!);

        if (snapshot is null)
            throw new InvalidOperationException(
                $"Cannot parse RedisValue to RefreshTokenSnapshot. TokenId: {id.Value}.");
        
        var result = RefreshTokenMapper.ToEntity(snapshot);

        return result;
    }

    public async Task<bool> RemoveByIdAsync(TokenId id, CancellationToken cancellationToken)
    {
        var key = _keyProvider.RefreshToken(id);
        var result = await _database.KeyDeleteAsync(key);
        return result;
    }

    public async Task<bool> SaveAsync(Domain.RefreshTokens.RefreshToken token, IDateTimeProvider clock,
        CancellationToken cancellationToken)
    {
        var ttl = token.ExpiresAt - clock.UtcNow;
        if (ttl < TimeSpan.Zero)
            return false;
        
        var snapshot = RefreshTokenMapper.ToSnapshot(token);

        var key = _keyProvider.RefreshToken(token.Id);

        var json = JsonSerializer.Serialize(snapshot);

        return await _database.StringSetAsync(
            key: key,
            value: json,
            expiry: ttl);
    }
}