using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.Validations;
using FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects;

public record struct ProductImagePath
{
    public const int MaxLength = 255;
    private static readonly string[] SupportedExtensions =
    [
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    ];
    public string Value { get; init; }

    private ProductImagePath(string value)
    {
        Value = value.Trim();
    }

    public static DomainResult<ProductImagePath> Create(string value)
    {
        var err = Validate(value);

        if (err is not null)
            return DomainResult<ProductImagePath>.Failure(err);
        
        return DomainResult<ProductImagePath>.Success(new ProductImagePath(value));
    }

    public static ProductImagePath Default => new("images/products/default.png");

    public static DomainError? Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return InvalidProductImagePathError.Empty();

        if (value.Length > MaxLength)
            return InvalidProductImagePathError.ExceedsMaxLength(MaxLength);

        var extension = Path.GetExtension(value);

        if (!SupportedExtensions.Contains(extension))
            return InvalidProductImagePathError.UnsupportedExtension(extension);
        
        if (!Regex.IsMatch(value, ValidationPatterns.ImagePath(extension)))
            return InvalidProductImagePathError.Format();
        
        return null;
    }
}