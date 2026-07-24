using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;

public class CachingQueryDecorator<TQuery, TResponse>(
    IHandler<TQuery, TResponse> handler,
    ICacheStore<TResponse> cacheStore,
    ILogger<CachingQueryDecorator<TQuery, TResponse>> logger,
    ICachePolicy<TQuery> policy)
    : QueryHandlerDecorator<TQuery, TResponse>(handler)
    where TQuery : IQuery<TResponse>
{
    public override async Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken)
    {
        var key = policy.GetKey(query);
        var ttl = policy.GetTtl();
        
        var response = await cacheStore.GetAsync(key, cancellationToken);

        if (response is not null)
        {
            logger.LogInformation("Data has been exists in cache.");
            return Result<TResponse>.Success(response);
        }
        
        logger.LogInformation("Data not exists in cache. Loading from persistence ...");

        var result = await Handler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogInformation("Loading failed.");
            return Result<TResponse>.Failure(result.Error!);
        }
        
        logger.LogInformation("Data loaded from persistence. Loading to cache.");

        await cacheStore.StoreAsync(
            key: key, 
            data: result.Value!, 
            ttl: ttl,
            cancellationToken: cancellationToken);
        
        logger.LogInformation("Data loaded to cache.");
        
        return Result<TResponse>.Success(result.Value!);
    }
}