using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public sealed class VerifyRegisterError
{
    public static readonly Error AuthOtpInvalid = Error.Unauthorized(
        "verify_otp_error.auth_otp_invalid",
        "Invalid or expired OTP.");
}