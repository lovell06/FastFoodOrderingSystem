using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public sealed class PhoneNumberError
{
    public static DomainError Empty()
    {
        return new DomainError(
            Code: "phone_number.empty",
            Message: "Phone number must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            Code: "phone_number.exceeds_max_length",
            Message: $"Phone number must not exceed {maxLength} charaters");
    }

    public static DomainError ContainsNonDigitCharacters()
    {
        return new DomainError(
            Code: "phone_number.contains_non_digit_characters",
            Message: "Phone number must not contain non digit characters.");
    }

    public static DomainError ContainsWhitespace()
    {
        return new DomainError(
            Code: "phone_number.contains_whitespace",
            Message: "Phone number must not contain whitespace characters.");
    }
}