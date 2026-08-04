using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public record struct Money
{
    public decimal Value { get; init; }

    private Money(decimal value)
    {
        Value = value;
    }

    public static Money Default => new Money(0m);

    public static DomainResult<Money> Create(decimal value)
    {
        return DomainResult<Money>.Success(new Money(value));
    }

    private static DomainError? Validate(decimal value)
    {
        return null;
    }
}