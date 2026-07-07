namespace FastFoodOrderingSystem.Application.Common.Handlers.HandlerDecorators;

public abstract class HandlerDecorator<TRequest, TResult> : IHandler<TRequest, TResult>
{
    protected readonly IHandler<TRequest, TResult> Handler;
    protected HandlerDecorator(IHandler<TRequest, TResult> handler)
    {
        Handler = handler;
    }
    public abstract Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}