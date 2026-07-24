using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Users.GetProfile;

public static class GetProfileError
{
    public static Error UserNotFound 
        => Error.NotFound("get_profile.user_not_found", "User not found");
}