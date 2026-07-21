using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;

public static class ChangePasswordError
{
    public static readonly Error Unauthorized = Error.Unauthorized(
        "change_password_error.unauthorized", 
        "Cannot authorize.");
}