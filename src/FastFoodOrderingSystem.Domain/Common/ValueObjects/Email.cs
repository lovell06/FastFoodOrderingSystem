using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record Email
{
    public const int MaxLength = 255;
    public string Value { get; init; }

    private Email(string value)
    {
        Value = value.Trim();
    }

    public static Email Create(string email)
    {   
        if (string.IsNullOrWhiteSpace(email))
            throw InvalidEmailException.Empty();

        email = email.Trim();
        if (email.Length > MaxLength)
            throw InvalidEmailException.ExceedsMaxLength(MaxLength);
        
        if (email.Any(char.IsWhiteSpace))
            throw InvalidEmailException.ContainsWhitespace();

        if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)+$"))
            throw InvalidEmailException.InvalidFormat();

        return new Email(email);
    }
}