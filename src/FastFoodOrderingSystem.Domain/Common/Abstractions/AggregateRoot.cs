namespace FastFoodOrderingSystem.Domain.Common.Abstractions;

public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvent
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id) : base(id)
    {
    }

    public void PublishDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvent()
    {
        _domainEvents.Clear();
    }
}