using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Domain.Users.Events;

public sealed class UserRegisteredDomainEvent : IDomainEvent
{
    public required DateTime OccurredAtUtc { get; set; }
    public required Guid UserId { get; set; }
    public required Email UserEmail { get; set; }
}