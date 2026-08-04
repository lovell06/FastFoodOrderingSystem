using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;
using FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects;

public record struct Password
{
    public const int MinLength = 8;
    public string Value { get; init; }
    private Password(string value)
    {
        Value = value;
    }

    private static DomainError? Validate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return InvalidPasswordError.Empty();
        
        if (raw.Length < MinLength)
            return InvalidPasswordError.PasswordLengthBelowMinimum(MinLength);

        if (!raw.Any(char.IsUpper))
            return InvalidPasswordError.PasswordRequiresUppercase();

        if (!raw.Any(char.IsLower))
            return InvalidPasswordError.PasswordRequiresLowercase();

        if (!raw.Any(char.IsDigit))
            return InvalidPasswordError.PasswordRequiresDigit();

        if (raw.All(char.IsLetterOrDigit))
            return InvalidPasswordError.PasswordRequiresSpecialCharacter();

        return null;
    }

    public static DomainResult<Password> Create(string raw)
    {
        var error = Validate(raw);

        if (error is not null)
            return DomainResult<Password>.Failure(error);
        return DomainResult<Password>.Success(new Password(raw));
    }
}