using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;

public static class UpdateProfileError
{
    public static Error Unauthorized => Error.Unauthorized(
        "update_profile.unauthorized",
        "Unauthorize");
}