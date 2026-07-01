using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public sealed class InvalidOtpCodeException : DomainException
{
    private InvalidOtpCodeException(string code, string message) : base(code, message)
    {
    }

    public static InvalidOtpCodeException Empty()
    {
        return new InvalidOtpCodeException(
            "InvalidOtpCodeException.Empty",
            "Otp code must not be empty.");
    }

    public static InvalidOtpCodeException InvalidLength(int length)
    {
        return new InvalidOtpCodeException(
            "InvalidOtpCodeException.InvalidLength",
            $"Otp code must be {length} digits long.");
    }

    public static InvalidOtpCodeException CodeIsNotDigit()
    {
        return new InvalidOtpCodeException(
            "InvalidOtpCodeException.CodeIsNotDigit",
            "Otp code must contain only digits.");
    }
}