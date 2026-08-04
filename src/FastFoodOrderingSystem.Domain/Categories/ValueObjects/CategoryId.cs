namespace FastFoodOrderingSystem.Domain.Categories.ValueObjects;

public record struct CategoryId
{
    public int Value { get; init; }

    private CategoryId(int value)
    {
        Value = value;
    }
    
    public static CategoryId Default => new CategoryId(0);

    public static CategoryId New(int value)
    {
        return new CategoryId(value);
    }
}