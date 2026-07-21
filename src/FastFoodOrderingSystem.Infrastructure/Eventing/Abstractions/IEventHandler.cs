namespace FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent e, CancellationToken cancellationToken);
}