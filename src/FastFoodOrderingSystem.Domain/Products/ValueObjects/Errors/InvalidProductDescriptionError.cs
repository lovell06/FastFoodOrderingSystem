using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

public static class InvalidProductDescriptionError
{
    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            "invalid_product_description_error.exceeds_max_length",
            $"Product description must not exceeds {maxLength} characters.");
    }
}