namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface IHandler<TRequest, TResult>
{
    public Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
}