using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Features.Users.GetPrivateUserProfile;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.PrivateUserProfile.Policies;

public sealed class GetCurrentUserQueryPolicy(RedisKeyProvider keyProvider) : ICachePolicy<GetPrivateUserProfileQuery>
{
    public string GetKey(GetPrivateUserProfileQuery query)
    {
        return keyProvider.PrivateUserProfile(query.UserId.ToString());
    }

    public TimeSpan GetTtl()
    {
        return CacheTtls.UseProfile;
    }
}