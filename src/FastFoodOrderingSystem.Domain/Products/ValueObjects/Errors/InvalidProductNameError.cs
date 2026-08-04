using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

public static class InvalidProductNameError
{
    public static DomainError Empty()
    {
        return new DomainError(
            "invalid_product_name_error.empty",
            "Product name must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            "invalid_product_name_error.exceeds_max_length",
            $"Product name must not exceeds {maxLength} characters.");
    }
}