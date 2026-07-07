using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

using DomainValueObjects = FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis;

public sealed class RedisKeyProvider
{
    private readonly string _prefixKey;

    public RedisKeyProvider(IOptions<RedisOption> options)
    {
        _prefixKey = options.Value.InstanceName;
    }

    public string PendingRegistration(DomainValueObjects.Email email)
        => $"{_prefixKey}:PendingRegistration:{email.Value}";

    public string RefreshToken(Guid userId)
        => $"{_prefixKey}:RefreshToken:{userId}";
}