using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public sealed class OtpCodeHashError
{
    public static DomainError Empty()
    {
        return new(
            Code: "otp_code_hash.empty",
            Message: "Otp code hash must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "otp_code_hash.exceeds_max_length",
            Message: $"Otp code hash must not exceed {maxLength} characters.");
    }
}