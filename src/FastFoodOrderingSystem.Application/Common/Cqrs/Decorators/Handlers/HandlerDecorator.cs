using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;

public abstract class HandlerDecorator<TRequest, TResponse> 
    : IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    protected readonly IHandler<TRequest, TResponse> Handler;
    protected HandlerDecorator(IHandler<TRequest, TResponse> handler)
    {
        Handler = handler;
    }
    public abstract Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}