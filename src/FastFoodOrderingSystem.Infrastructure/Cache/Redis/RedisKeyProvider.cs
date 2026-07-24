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

    public string RefreshToken(string id)
        => $"{_prefixKey}:RefreshToken:{id}";

    public string ForgotPasswordOtp(Email email)
        => $"{_prefixKey}:ForgotPasswordOtp:{email.Value}";

    public string UserProfile(string key)
        => $"{_prefixKey}:User:{key}:Profile";
}