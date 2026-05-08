using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public sealed class InvalidPhoneNumberException : DomainException
{
    private InvalidPhoneNumberException(string code, string message) : base(code, message)
    {
    }

    public static InvalidPhoneNumberException Empty()
    {
        return new InvalidPhoneNumberException(
            code: "phone_number.empty",
            message: "Phone number must not be empty.");
    }

    public static InvalidPhoneNumberException ExceedsMaxLength(int maxLength)
    {
        return new InvalidPhoneNumberException(
            code: "phone_number.exceeds_max_length",
            message: $"Phone number must not exceed {maxLength} charaters");
    }

    public static InvalidPhoneNumberException ContainsNonDigitCharacters()
    {
        return new InvalidPhoneNumberException(
            code: "phone_number.contains_non_digit_characters",
            message: "Phone number must not contain non digit characters.");
    }

    public static InvalidPhoneNumberException ContainsWhitespace()
    {
        return new InvalidPhoneNumberException(
            code: "phone_number.contains_whitespace",
            message: "Phone number must not contain whitespace characters.");
    }
}