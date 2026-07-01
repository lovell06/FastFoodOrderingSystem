using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record Password
{
    public const int MinLength = 8;
    public string Value { get; }
    private Password(string value)
    {
        Value = value;
    }

    private static void Validate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw InvalidPasswordException.Empty();
        
        if (raw.Length < MinLength)
            throw InvalidPasswordException.PasswordLengthBelowMinimum(MinLength);

        if (!raw.Any(char.IsUpper))
            throw InvalidPasswordException.PasswordRequiresUppercase();

        if (!raw.Any(char.IsLower))
            throw InvalidPasswordException.PasswordRequiresLowercase();

        if (!raw.Any(char.IsDigit))
            throw InvalidPasswordException.PasswordRequiresDigit();

        if (raw.All(char.IsLetterOrDigit))
            throw InvalidPasswordException.PasswordRequiresSpecialCharacter();
    }

    public static Password Create(string raw)
    {
        Validate(raw);
        return new Password(raw);
    }
}