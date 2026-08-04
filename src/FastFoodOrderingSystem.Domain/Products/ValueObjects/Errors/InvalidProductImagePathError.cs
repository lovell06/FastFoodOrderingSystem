using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

public static class InvalidProductImagePathError
{
    public static DomainError Empty()
    {
        return new DomainError(
            "invalid_product_image_path_error.empty",
            "Product image path must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            "invalid_product_image_path_error.exceeds_max_length",
            $"Product image path must not exceeds {maxLength} characters.");
    }

    public static DomainError UnsupportedExtension(string extension)
    {
        return new DomainError(
            "invalid_product_image_path_error.unsupported_extension",
            $"Product image path unsupported {extension} extension.");
    }

    public static DomainError Format()
    {
        return new DomainError(
            "invalid_product_image_path_error.format",
            "Product image path format invalid.");
    }
}