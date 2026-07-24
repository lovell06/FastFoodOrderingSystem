using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh;

public sealed class RefreshTokenError
{
    public static Error Unauthorized => Error.Unauthorized(
        code: "refresh_token_error.failure",
        message: "Refresh token invalid.");
}