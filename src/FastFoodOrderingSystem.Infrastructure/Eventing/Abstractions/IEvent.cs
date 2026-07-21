namespace FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;

public interface IEvent
{
    DateTime OccurredAtUtc { get; init; }
}