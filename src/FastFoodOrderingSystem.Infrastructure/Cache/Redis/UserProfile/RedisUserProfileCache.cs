using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
using StackExchange.Redis;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.UserProfile;

public sealed class RedisUserProfileCache : ICacheStore<GetProfileQuery, UserProfileResponse>
{
    private readonly RedisKeyProvider _keyProvider;
    private readonly IDatabase _database;

    public RedisUserProfileCache(IConnectionMultiplexer multiplexer, RedisKeyProvider keyProvider)
    {
        _keyProvider = keyProvider;
        _database = multiplexer.GetDatabase();
    }
    
    public async Task<bool> StoreAsync(
        GetProfileQuery query, 
        UserProfileResponse data, 
        CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(data);
        
        return await _database.StringSetAsync(
            key: _keyProvider.UserProfile(query.UserId.ToString()),
            value: json,
            expiry: CacheTtls.UseProfile);
    }

    public async Task<bool> RemoveAsync(
        GetProfileQuery query, 
        CancellationToken cancellationToken)
    {
        return await _database.KeyDeleteAsync(_keyProvider.UserProfile(query.UserId.ToString()));
    }

    public async Task<UserProfileResponse?> GetAsync(
        GetProfileQuery query, 
        CancellationToken cancellationToken)
    {
        var json = await _database.StringGetAsync(_keyProvider.UserProfile(query.UserId.ToString()));
        
        if (!json.HasValue)
            return null;

        return JsonSerializer.Deserialize<UserProfileResponse>(json: json!);
    }
}