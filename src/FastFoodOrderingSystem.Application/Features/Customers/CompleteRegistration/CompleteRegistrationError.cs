using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Customers.CompleteRegistration;

public static class CompleteRegistrationError
{
    public static readonly Error AuthOtpInvalid = Error.Unauthorized(
        "verify_otp_error.auth_otp_invalid",
        "Invalid or expired OTP.");
}