using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public static class InvalidFullNameError
{
    public static DomainError Empty()
    {
        return new(
            Code: "invalid_full_name_error.empty",
            Message: "Full name must not be empty!");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "invalid_full_name_error.exceeds_max_length",
            Message: $"Full name must not exceed {maxLength} characters");
    }

    public static DomainError ContainsInvalidCharacters()
    {
        return new(
            Code: "invalid_full_name_error.invalid_characters",
            Message: "Full name contains invalid characters, only contains letters or numbers!");
    }
}