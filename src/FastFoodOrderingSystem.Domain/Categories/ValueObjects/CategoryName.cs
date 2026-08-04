using FastFoodOrderingSystem.Domain.Categories.ValueObjects.Errors;
using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Categories.ValueObjects;

public record struct CategoryName
{
    public const int MaxLength = 100;
    public string Value { get; init; }

    private CategoryName(string value)
    {
        Value = value;
    }

    public static DomainResult<CategoryName> Create(string value)
    {
        var err = Validate(value);
        
        if (err is not null)
            return DomainResult<CategoryName>.Failure(err);
        
        return DomainResult<CategoryName>.Success(new CategoryName(value));
    }

    private static DomainError? Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return InvalidCategoryNameError.Empty();

        if (value.Length > MaxLength)
            return InvalidCategoryNameError.ExceedsMaxLength(MaxLength);
        
        return null;
    }
}