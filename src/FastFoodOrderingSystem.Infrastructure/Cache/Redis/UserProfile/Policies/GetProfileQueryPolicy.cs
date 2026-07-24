using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.UserProfile.Policies;

public sealed class GetProfileQueryPolicy(RedisKeyProvider keyProvider) : ICachePolicy<GetProfileQuery>
{
    public string GetKey(GetProfileQuery query)
    {
        return keyProvider.UserProfile(query.UserId.ToString());
    }

    public TimeSpan GetTtl()
    {
        return CacheTtls.UseProfile;
    }
}