using FastFoodOrderingSystem.Domain.RefreshTokens;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis;

public sealed class RedisKeyProvider
{
    private readonly string _prefixKey;

    public RedisKeyProvider(IOptions<RedisOption> options)
    {
        _prefixKey = options.Value.InstanceName;
    }

    public string PendingRegistration(Email email)
        => $"{_prefixKey}:PendingRegistration:{email.Value}";

    public string RefreshToken(TokenId id)
        => $"{_prefixKey}:RefreshToken:{id.Value}";
}