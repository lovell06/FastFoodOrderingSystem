using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Features.Users.GetCurrentUserProfile;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.PrivateUserProfile.Policies;

public sealed class GetCurrentUserQueryPolicy(RedisKeyProvider keyProvider) : ICachePolicy<GetCurrentUserProfileQuery>
{
    public string GetKey(GetCurrentUserProfileQuery query)
    {
        return keyProvider.PrivateUserProfile(query.UserId.ToString());
    }

    public TimeSpan GetTtl()
    {
        return CacheTtls.UseProfile;
    }
}