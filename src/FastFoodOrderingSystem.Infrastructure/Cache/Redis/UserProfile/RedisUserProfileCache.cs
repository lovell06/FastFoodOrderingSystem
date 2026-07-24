using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.UserProfile;

public sealed class RedisUserProfileCache(IConnectionMultiplexer multiplexer)
    : ICacheStore<UserProfileResponse>
{
    private readonly IDatabase _database = multiplexer.GetDatabase();

    public async Task<bool> StoreAsync(
        string key, 
        UserProfileResponse data, 
        TimeSpan ttl,
        CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(data);
        
        return await _database.StringSetAsync(
            key: key,
            value: json,
            expiry: ttl);
    }

    public async Task<bool> RemoveAsync(
        string key, 
        CancellationToken cancellationToken)
    {
        return await _database.KeyDeleteAsync(key: key);
    }

    public async Task<UserProfileResponse?> GetAsync(
        string key, 
        CancellationToken cancellationToken)
    {
        var json = await _database.StringGetAsync(key: key);
        
        if (!json.HasValue)
            return null;

        return JsonSerializer.Deserialize<UserProfileResponse>(json: json!);
    }
}