using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.RefreshToken;

public sealed class RedisRefreshTokenCache(
    IConnectionMultiplexer connectionMultiplexer,
    RedisKeyProvider keyProvider)
    : IRefreshTokenStore
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    public async Task<long> RevokeByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userRefreshTokenKey = keyProvider.RefreshTokenByUser(userId);

        var keys = (await _database.SetMembersAsync(userRefreshTokenKey))
            .Where(t => t.HasValue)
            .Select(t => new RedisKey(keyProvider.RefreshToken(id: t!))).ToList();

        keys.Add(userRefreshTokenKey);
        
        return await _database.KeyDeleteAsync([.. keys]);
    }

    public async Task<Application.Abstractions.Cache.RefreshToken.RefreshToken?> GetAsync(
        Guid userId,
        string token, 
        CancellationToken cancellationToken)
    {
        var id = Application.Abstractions.Cache.RefreshToken.RefreshToken.GenerateId(token);
        
        var key = keyProvider.RefreshToken(id);

        var json = await _database.StringGetAsync(key);

        if (!json.HasValue)
            return null;
            
        var snapshot = JsonSerializer.Deserialize<RefreshTokenSnapshot>(json: json!);

        if (snapshot is null)
            throw new InvalidOperationException(
                $"Cannot parse RedisValue to RefreshTokenSnapshot. TokenId: {id}.");
        
        var result = RefreshTokenMapper.ToEntity(snapshot);

        return result;
    }

    public async Task<bool> RevokeAsync(
        Guid userId,
        string token, 
        CancellationToken cancellationToken)
    {
        var tokenId = Application.Abstractions.Cache.RefreshToken.RefreshToken.GenerateId(token);
        var refreshTokenKey = keyProvider.RefreshToken(tokenId);
        await _database.KeyDeleteAsync(refreshTokenKey);

        var userRefreshTokenKey = keyProvider.RefreshTokenByUser(userId);
        return await _database.SetRemoveAsync(userRefreshTokenKey, tokenId);
    }

    public async Task<bool> StoreAsync(
        Guid userId,
        Application.Abstractions.Cache.RefreshToken.RefreshToken token, 
        IDateTimeProvider clock,
        CancellationToken cancellationToken)
    {
        var ttl = token.ExpiresAt - clock.UtcNow;
        if (ttl < TimeSpan.Zero)
            return false;
        
        var snapshot = RefreshTokenMapper.ToSnapshot(token);

        var key = keyProvider.RefreshToken(token.Id);

        var json = JsonSerializer.Serialize(snapshot);

        var transaction = _database.CreateTransaction();

        _ = transaction.StringSetAsync(
            key: key,
            value: json,
            expiry: ttl);

        _ = transaction.SetAddAsync(keyProvider.RefreshTokenByUser(userId), token.Id);

        return await transaction.ExecuteAsync();
    }
}