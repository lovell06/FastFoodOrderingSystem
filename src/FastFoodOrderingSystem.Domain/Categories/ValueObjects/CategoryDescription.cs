using FastFoodOrderingSystem.Domain.Categories.ValueObjects.Errors;
using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Categories.ValueObjects;

public record struct CategoryDescription
{
    public const int MaxLength = 1000;
    public string Value { get; init; }

    private CategoryDescription(string value)
    {
        Value = value;
    }

    public static DomainResult<CategoryDescription> Create(string value)
    {
        var err = Validate(value);
        
        if (err is not null)
            return DomainResult<CategoryDescription>.Failure(err);
        
        return DomainResult<CategoryDescription>.Success(new CategoryDescription(value));
    }

    public static CategoryDescription Default => new(string.Empty);

    private static DomainError? Validate(string value)
    {
        if (value.Length > MaxLength)
            return InvalidCategoryDescriptionError.ExceedsMaxLength(MaxLength);
        
        return null;
    }
}