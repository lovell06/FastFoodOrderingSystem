using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.Validations;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public record struct FullName
{
    public const int MaxLength = 255;
    public string Value { get; init; }

    private FullName(string value)
    {
        Value = value.Trim();
    }

    public static DomainResult<FullName> Create(string fullName)
    {
        var error = Validate(fullName);

        if (error is not null)
            return DomainResult<FullName>.Failure(error);
        
        return DomainResult<FullName>.Success(new FullName(fullName));
    }

    private static DomainError? Validate(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return InvalidFullNameError.Empty();

        fullName = fullName.Trim();
        if (fullName.Length > MaxLength)
            return InvalidFullNameError.ExceedsMaxLength(MaxLength);

        if (Regex.IsMatch(fullName, ValidationPatterns.FullName))
            return InvalidFullNameError.ContainsInvalidCharacters();

        return null;
    }
}