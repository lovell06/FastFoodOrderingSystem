using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Products.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Products.ValueObjects;

public readonly record struct StockQuantity
{
    public int Value { get; }

    private StockQuantity(int value)
    {
        Value = value;
    }

    public static readonly StockQuantity Zero = new(0);

    public static DomainResult<StockQuantity> Create(int value)
    {
        var err = Validate(value);
        
        if (err is not null)
            return DomainResult<StockQuantity>.Failure(err);
        
        return DomainResult<StockQuantity>.Success(new StockQuantity(value));
    }

    private static DomainError? Validate(int value)
    {
        if (value < 0)
            return InvalidStockQuantityError.Negative();

        return null;
    }

    public DomainResult<StockQuantity> Increase(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);
        
        return Create(Value + quantity);
    }

    public DomainResult<StockQuantity> Decrease(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);
        
        return Create(Value - quantity);
    }

    public bool HasEnough(int quantity)
    {
        return Value >= quantity;
    }
}