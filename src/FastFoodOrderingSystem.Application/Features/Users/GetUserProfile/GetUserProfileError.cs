using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Users.GetUserProfile;

public static class GetUserProfileError
{
    public static Error UserNotFound 
        => Error.NotFound("get_profile.user_not_found", "User not found");
}