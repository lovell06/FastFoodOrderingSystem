using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.IntegrationEventDispatchers;

public sealed class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _provider;

    public EventDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task DispatchAsync(IReadOnlyCollection<IEvent> events, CancellationToken cancellationToken)
    {
        foreach (var e in events)
        {
            await Publish(e, cancellationToken);
        }
    }
    
    public async Task DispatchAsync(IEvent events, CancellationToken cancellationToken)
    {
        await Publish(events, cancellationToken);
    }

    private async Task Publish(IEvent e, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IEventHandler<>).MakeGenericType(e.GetType());

        var handlers = _provider.GetServices(handlerType);

        foreach (dynamic? handler in handlers)
        {
            await handler!.HandleAsync((dynamic)e, cancellationToken);
        }
    }
}