using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.Validations;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;
using FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects;

public sealed record Email
{
    public const int MaxLength = 255;
    public string Value { get; init; }

    private Email(string value)
    {
        Value = value.Trim();
    }

    public static DomainResult<Email> Create(string email)
    {   
        var error = Validate(email);

        if (error is not null)
            return DomainResult<Email>.Failure(error);

        return DomainResult<Email>.Success(new Email(email));
    }

    private static DomainError? Validate(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return EmailError.Empty();

        email = email.Trim();
        if (email.Length > MaxLength)
            return EmailError.ExceedsMaxLength(MaxLength);

        if (email.Any(char.IsWhiteSpace))
            return EmailError.ContainsWhitespace();

        if (!Regex.IsMatch(email, ValidationPatterns.Email))
            return EmailError.InvalidFormat();

        return null;
    }
}