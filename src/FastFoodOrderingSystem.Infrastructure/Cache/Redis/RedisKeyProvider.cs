using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis;

public sealed class RedisKeyProvider(IOptions<RedisOption> options)
{
    private readonly string _prefixKey = options.Value.InstanceName;

    public string PendingRegistration(Email email)
        => $"{_prefixKey}:PendingRegistration:{email.Value}";

    public string RefreshToken(string id)
        => $"{_prefixKey}:RefreshToken:{id}";

    public string RefreshTokenByUser(Guid userId)
        => $"{_prefixKey}:UserRefreshToken:{userId:N}";

    public string ForgotPasswordOtp(Email email)
        => $"{_prefixKey}:ForgotPasswordOtp:{email.Value}";

    public string PublicUserProfile(string key)
        => $"{_prefixKey}:User:{key}:PublicProfile";

    public string PrivateUserProfile(string key)
        => $"{_prefixKey}:User:{key}:PrivateProfile";
}