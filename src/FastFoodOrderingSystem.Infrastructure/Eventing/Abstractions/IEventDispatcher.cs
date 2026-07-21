namespace FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;

public interface IEventDispatcher
{
    Task DispatchAsync(IReadOnlyCollection<IEvent> events, CancellationToken cancellationToken);
    Task DispatchAsync(IEvent e, CancellationToken cancellationToken);
}