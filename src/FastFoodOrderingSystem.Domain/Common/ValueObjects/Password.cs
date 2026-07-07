using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record Password
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
            return PasswordError.Empty();
        
        if (raw.Length < MinLength)
            return PasswordError.PasswordLengthBelowMinimum(MinLength);

        if (!raw.Any(char.IsUpper))
            return PasswordError.PasswordRequiresUppercase();

        if (!raw.Any(char.IsLower))
            return PasswordError.PasswordRequiresLowercase();

        if (!raw.Any(char.IsDigit))
            return PasswordError.PasswordRequiresDigit();

        if (raw.All(char.IsLetterOrDigit))
            return PasswordError.PasswordRequiresSpecialCharacter();

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