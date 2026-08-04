namespace FastFoodOrderingSystem.Domain.Products.ValueObjects;

public record struct ProductId
{
    public int Value { get; init; }

    private ProductId(int value)
    {
        Value = value;
    }

    public static ProductId Default => new ProductId(0);

    public static ProductId New(int value)
    {
        return new ProductId(value);
    }
}