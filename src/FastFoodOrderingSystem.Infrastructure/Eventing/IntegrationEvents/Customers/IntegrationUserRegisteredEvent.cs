using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEvents.Customers;

public sealed class IntegrationUserRegisteredEvent : IEvent
{
    public DateTime OccurredAtUtc { get; init; }
    public Guid UserId { get; init; }
    public required string UserEmail { get; init; }
}