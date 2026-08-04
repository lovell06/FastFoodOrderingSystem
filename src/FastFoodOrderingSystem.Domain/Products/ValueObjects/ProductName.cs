using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects;

public record struct ProductName
{
    public const int MaxLength = 100;
    public string Value { get; init; }

    private ProductName(string value)
    {
        Value = value;
    }

    public static DomainResult<ProductName> Create(string value)
    {
        var err = Validate(value);
        
        if (err is not null)
            return DomainResult<ProductName>.Failure(err);
        
        return DomainResult<ProductName>.Success(new ProductName(value));
    }

    private static DomainError? Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return InvalidProductNameError.Empty();

        if (value.Length > MaxLength)
            return InvalidProductNameError.ExceedsMaxLength(MaxLength);
        
        return null;
    }
}