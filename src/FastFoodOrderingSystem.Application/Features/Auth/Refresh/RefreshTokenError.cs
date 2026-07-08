using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh;

public sealed class RefreshTokenError
{
    public static Error Failure => Error.Failure(
        code: "refresh_token_error.failure",
        message: "Refresh token invalid.");

    public static Error UserNotFound => Error.NotFound(
        code: "refresh_token_error.user_notF_found",
        message: "User not found");
}