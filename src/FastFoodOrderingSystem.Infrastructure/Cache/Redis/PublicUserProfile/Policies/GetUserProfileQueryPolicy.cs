using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Features.Users.GetPublicUserProfile;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.PublicUserProfile.Policies;

public sealed class GetUserProfileQueryPolicy(RedisKeyProvider keyProvider) : ICachePolicy<GetPublicUserProfileQuery>
{
    public string GetKey(GetPublicUserProfileQuery query)
    {
        return keyProvider.PublicUserProfile(query.UserId.ToString());
    }

    public TimeSpan GetTtl()
    {
        return CacheTtls.UseProfile;
    }
}