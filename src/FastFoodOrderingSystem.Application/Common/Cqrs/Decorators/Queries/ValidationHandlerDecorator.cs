using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;

public sealed class ValidationHandlerDecorator<TQuery, TResponse> 
    : QueryHandlerDecorator<TQuery, TResponse> where TQuery : IQuery
{
    public ValidationHandlerDecorator(IHandler<TQuery, TResponse> handler) : base(handler)
    {
    }

    public override async Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken)
    {
        if (query is null)
        {
            
        }
        var result = await Handler.HandleAsync(query, cancellationToken);

        return result;
    }
}