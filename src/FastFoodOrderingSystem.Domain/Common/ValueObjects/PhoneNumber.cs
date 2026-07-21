using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.Validations;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record PhoneNumber
{
    public const int MaxLength = 10;
    public string Value { get; init; }

    private PhoneNumber(string value)
    {
        Value = value.Trim();
    }

    public static DomainResult<PhoneNumber> Create(string phone)
    {
        var error = Validate(phone);

        if (error is not null)
            return DomainResult<PhoneNumber>.Failure(error);
        return DomainResult<PhoneNumber>.Success(new PhoneNumber(phone));
    }

    private static DomainError? Validate(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return PhoneNumberError.Empty();

        phone = phone.Trim();
        if (phone.Length > MaxLength)
            return PhoneNumberError.ExceedsMaxLength(MaxLength);

        if (Regex.IsMatch(phone, ValidationPatterns.PhoneNumber))
            return PhoneNumberError.ContainsNonDigitCharacters();

        if (phone.Any(char.IsWhiteSpace))
            return PhoneNumberError.ContainsWhitespace();

        return null;
    }
}