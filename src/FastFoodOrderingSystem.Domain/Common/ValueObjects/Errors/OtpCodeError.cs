using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public sealed class OtpCodeError
{
    public static DomainError Empty()
    {
        return new(
            "otp_code.empty",
            "Otp code must not be empty.");
    }

    public static DomainError InvalidLength(int length)
    {
        return new(
            "otp_code.invalid_length",
            $"Otp code must be {length} digits long.");
    }

    public static DomainError CodeIsNotDigit()
    {
        return new(
            "otp_code.code_is_not_digit",
            "Otp code must contain only digits.");
    }
}