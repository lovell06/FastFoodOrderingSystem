using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public class InvalidOtpCodeHashException : DomainException
{
    private InvalidOtpCodeHashException(string code, string message) : base(code, message)
    {
    }

    public static InvalidOtpCodeHashException Empty()
    {
        return new InvalidOtpCodeHashException(
            "invalid_otp_code_hash_exception.empty",
            "Otp code hash must not be empty.");
    }

    public static InvalidOtpCodeHashException ExceedsMaxLength(int maxLength)
    {
        return new InvalidOtpCodeHashException(
            code: "invalid_otp_code_hash_exception.exceeds_max_length",
            message: $"Otp code hash must not exceed {maxLength} characters.");
    } 
}