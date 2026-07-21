using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface IHandler<TRequest, TResponse>
{
    public Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}