namespace FastFoodOrderingSystem.Domain.Common.Abstractions;

public interface IHasDomainEvent
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void RegisterDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvent();
}