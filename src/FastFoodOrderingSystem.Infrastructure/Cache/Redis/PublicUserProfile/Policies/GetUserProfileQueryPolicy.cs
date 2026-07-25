using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Features.Users.GetUserProfile;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.PublicUserProfile.Policies;

public sealed class GetUserProfileQueryPolicy(RedisKeyProvider keyProvider) : ICachePolicy<GetUserProfileQuery>
{
    public string GetKey(GetUserProfileQuery query)
    {
        return keyProvider.PublicUserProfile(query.UserId.ToString());
    }

    public TimeSpan GetTtl()
    {
        return CacheTtls.UseProfile;
    }
}