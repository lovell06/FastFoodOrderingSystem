using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public class FullNameError
{
    public static DomainError Empty()
    {
        return new(
            Code: "full_name.empty",
            Message: "Full name must not be empty!");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "full_name.exceeds_max_length",
            Message: $"Full name must not exceed {maxLength} characters");
    }

    public static DomainError ContainsInvalidCharacters()
    {
        return new(
            Code: "full_name.invalid_characters",
            Message: "Full name contains invalid characters, only contains letters or numbers!");
    }
}