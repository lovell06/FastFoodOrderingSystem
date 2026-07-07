namespace FastFoodOrderingSystem.Application.Common.Handlers;

public interface IHandler<TRequest, TResult>
{
    public Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}