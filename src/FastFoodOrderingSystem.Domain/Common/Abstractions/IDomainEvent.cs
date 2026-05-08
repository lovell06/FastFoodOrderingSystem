namespace FastFoodOrderingSystem.Domain.Common.Abstractions;

public interface IDomainEvent
{
    DateTime OccurredAtUtc { get; set; }
}