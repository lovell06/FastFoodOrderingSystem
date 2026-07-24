using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.UserProfile.Policies;

public sealed class UpdateProfileCommandPolicy(RedisKeyProvider keyProvider) : ICachePolicy<UpdateProfileCommand>
{
    public string GetKey(UpdateProfileCommand command)
    {
        return keyProvider.UserProfile(command.UserId.ToString());
    }

    public TimeSpan GetTtl()
    {
        return CacheTtls.UseProfile;
    }
}