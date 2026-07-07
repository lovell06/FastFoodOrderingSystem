using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;

public sealed class VerifyOtpError
{
    public static readonly Error AuthOtpInvalid = Error.Business(
        "verify_otp_error.auth_otp_invalid",
        "Invalid or expired OTP.");
}