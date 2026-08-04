using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Products.Errors;

public static class InvalidProductError
{
    public static DomainError Inactive()
    {
        return new DomainError(
            "invalid_product_error.inactive",
            "Product inactive.");
    }

    public static DomainError Discontinued()
    {
        return new DomainError(
            "invalid_product_error.discontinue",
            "Product is discontinued.");
    }

    public static DomainError Deleted()
    {
        return new DomainError(
            "invalid_product_error.deleted",
            "Product is deleted.");
    }
}