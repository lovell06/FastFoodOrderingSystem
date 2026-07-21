using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;

public static class InitiateForgotPasswordError
{
    public static readonly Error Unauthorized = Error.Unauthorized(
        "forgot_password_error.unauthorized",
        "Email or OTP code invalid");
}