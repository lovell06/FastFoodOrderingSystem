using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Abstractions.Mediator;

public interface IMediator
{
    Task<Result<TResponse>> SendAsync<TRequest, TResponse>(
        TRequest request, 
        CancellationToken cancellationToken) where TRequest : IRequest<TResponse>;
}