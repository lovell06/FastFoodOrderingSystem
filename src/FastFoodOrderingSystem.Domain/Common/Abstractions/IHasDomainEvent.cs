namespace FastFoodOrderingSystem.Domain.Common.Abstractions;

public interface IHasDomainEvent
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void PublishDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvent();
}