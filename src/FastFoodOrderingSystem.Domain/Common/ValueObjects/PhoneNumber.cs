using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record PhoneNumber
{
    public const int MaxLength = 10;
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value.Trim();
    }

    public static PhoneNumber Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw InvalidPhoneNumberException.Empty();

        phone = phone.Trim();
        if (phone.Length > MaxLength)
            throw InvalidPhoneNumberException.ExceedsMaxLength(MaxLength);
        
        if (Regex.IsMatch(phone, @"\D"))
            throw InvalidPhoneNumberException.ContainsNonDigitCharacters();
        
        if (phone.Any(char.IsWhiteSpace))
            throw InvalidPhoneNumberException.ContainsWhitespace();

        return new PhoneNumber(phone);
    }
}