namespace FastFoodOrderingSystem.Application.Common.Handlers.QueryDecorators;

public abstract class QueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
{
    public abstract Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}