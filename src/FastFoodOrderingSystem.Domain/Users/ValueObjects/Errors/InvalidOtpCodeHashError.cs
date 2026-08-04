using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public static class InvalidOtpCodeHashError
{
    public static DomainError Empty()
    {
        return new(
            Code: "invalid_otp_code_hash_error.empty",
            Message: "Otp code hash must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "invalid_otp_code_hash_error.exceeds_max_length",
            Message: $"Otp code hash must not exceed {maxLength} characters.");
    }
}