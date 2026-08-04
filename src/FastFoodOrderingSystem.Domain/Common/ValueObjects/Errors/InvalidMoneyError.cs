using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public static class InvalidMoneyError
{
    public static DomainError Negative()
    {
        return new DomainError(
            "invalid_money_error.negative",
            "Money must not be negative.");
    }
}