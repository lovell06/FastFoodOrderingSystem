using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record FullName
{
    public const int MaxLength = 255;
    public string Value { get; }

    private FullName(string value)
    {
        Value = value.Trim();
    }

    public static FullName Create(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw InvalidFullNameException.Empty();

        fullName = fullName.Trim();
        if (fullName.Length > MaxLength)
            throw InvalidFullNameException.ExceedsMaxLength(MaxLength);

        if (Regex.IsMatch(fullName, @"[^\p{L}\s]"))
            throw InvalidFullNameException.ContainsInvalidCharacters();

        return new FullName(fullName);
    }
}