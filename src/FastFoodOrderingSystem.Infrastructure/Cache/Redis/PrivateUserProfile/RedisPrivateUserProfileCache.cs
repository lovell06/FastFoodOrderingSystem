using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Features.Users.GetCurrentUserProfile;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.PrivateUserProfile;

public sealed class RedisPrivateUserProfileCache(IConnectionMultiplexer multiplexer) : ICacheStore<PrivateUserProfileResponse>
{
    private readonly IDatabase _database = multiplexer.GetDatabase();
    
    public async Task<bool> StoreAsync(string key, PrivateUserProfileResponse data, TimeSpan ttl, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(data);

        return await _database.StringSetAsync(
            key: key,
            value: json,
            expiry: ttl);
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken)
    {
        return await _database.KeyDeleteAsync(key: key);
    }

    public async Task<PrivateUserProfileResponse?> GetAsync(string key, CancellationToken cancellationToken)
    {
        var json = await _database.StringGetAsync(key: key);

        if (!json.HasValue)
            return null;
        
        return JsonSerializer.Deserialize<PrivateUserProfileResponse>(json: json!);
    }
}