using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Users.Events;
using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEvents.Customers;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.EventMappers;

public static class DomainEventMappers
{
    public static IEvent ToIntegration(this IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            UserRegisteredDomainEvent e => new IntegrationUserRegisteredEvent
            {
                OccurredAtUtc = e.OccurredAtUtc,
                UserId = e.UserId,
                UserEmail = e.UserEmail
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}