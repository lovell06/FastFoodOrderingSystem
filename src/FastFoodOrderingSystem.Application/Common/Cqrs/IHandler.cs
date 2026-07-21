using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface IHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}