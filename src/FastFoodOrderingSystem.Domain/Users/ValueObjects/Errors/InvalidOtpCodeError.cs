using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public static class InvalidOtpCodeError
{
    public static DomainError Empty()
    {
        return new(
            "invalid_otp_code_error.empty",
            "Otp code must not be empty.");
    }

    public static DomainError InvalidLength(int length)
    {
        return new(
            "invalid_otp_code_error.invalid_length",
            $"Otp code must be {length} digits long.");
    }

    public static DomainError CodeIsNotDigit()
    {
        return new(
            "invalid_otp_code_error.code_is_not_digit",
            "Otp code must contain only digits.");
    }
}