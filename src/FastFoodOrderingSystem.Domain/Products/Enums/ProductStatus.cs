using FastFoodOrderingSystem.Domain.Common.Abstractions;

namespace FastFoodOrderingSystem.Domain.Products.Enums;

public class ProductStatus : SmartEnum<ProductStatus>
{
    private ProductStatus(string code) : base(code)
    {
    }

    public static ProductStatus Active => new("Active");
    public static ProductStatus Deleted => new("Deleted");
    public static ProductStatus Hidden => new("Hidden");
    public static ProductStatus Discontinued => new("Discontinued");
}