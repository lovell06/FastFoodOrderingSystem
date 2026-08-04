using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

public static class InvalidStockQuantityError
{
    public static DomainError Negative()
    {
        return new DomainError(
            "invalid_stock_quantity_error.negative",
            "Stock quantity must not be negative.");
    }
}