namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;

public abstract class QueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
{
    public abstract Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}