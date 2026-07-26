using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Users.GetPrivateUserProfile;

public static class GetPrivateUserProfileError
{
    public static Error UserNotFound 
        => Error.NotFound("get_profile.user_not_found", "User not found");
}