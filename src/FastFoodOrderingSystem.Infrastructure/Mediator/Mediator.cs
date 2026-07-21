using FastFoodOrderingSystem.Application.Abstractions.Mediator;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Mediator;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _provider;
    
    public Mediator(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Result<TResponse>> SendAsync<TRequest, TResponse>(
        TRequest request, 
        CancellationToken cancellationToken) where TRequest : IRequest<TResponse>
    {
        var handlerType = typeof(IHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = _provider.GetRequiredService(handlerType);

        return await handler.HandleAsync(request, cancellationToken);
    }
}