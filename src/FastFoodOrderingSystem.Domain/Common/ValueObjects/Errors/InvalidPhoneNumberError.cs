using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public static class InvalidPhoneNumberError
{
    public static DomainError Empty()
    {
        return new DomainError(
            Code: "invalid_phone_number_error.empty",
            Message: "Phone number must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            Code: "invalid_phone_number_error.exceeds_max_length",
            Message: $"Phone number must not exceed {maxLength} characters");
    }

    public static DomainError ContainsNonDigitCharacters()
    {
        return new DomainError(
            Code: "invalid_phone_number_error.contains_non_digit_characters",
            Message: "Phone number must not contain non digit characters.");
    }

    public static DomainError ContainsWhitespace()
    {
        return new DomainError(
            Code: "invalid_phone_number_error.contains_whitespace",
            Message: "Phone number must not contain whitespace characters.");
    }
}