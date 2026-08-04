using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects;

public record struct ProductDescription
{
    public const int MaxLength = 1000;
    public string Value { get; private set; }

    private ProductDescription(string value)
    {
        Value = value;
    }

    public static DomainResult<ProductDescription> Create(string value)
    {
        var err = Validate(value);

        if (err is not null)
            return DomainResult<ProductDescription>.Failure(err);
        
        return DomainResult<ProductDescription>.Success(new ProductDescription(value));
    }

    public static ProductDescription Default => new(string.Empty);

    private static DomainError? Validate(string value)
    {
        if (value.Length > MaxLength)
            return InvalidProductDescriptionError.ExceedsMaxLength(MaxLength);
        
        return null;
    }
}