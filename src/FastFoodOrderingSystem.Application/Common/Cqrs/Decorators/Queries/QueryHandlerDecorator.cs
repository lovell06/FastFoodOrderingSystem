using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;

public abstract class QueryHandlerDecorator<TQuery, TResponse> 
    : IQueryHandler<TQuery, TResponse> where TQuery : IQuery
{
    protected readonly IHandler<TQuery, TResponse> Handler;

    protected QueryHandlerDecorator(IHandler<TQuery, TResponse> handler)
    {
        Handler = handler;
    }

    public abstract Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}