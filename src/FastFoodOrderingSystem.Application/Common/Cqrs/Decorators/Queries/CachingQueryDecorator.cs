using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;

public class CachingQueryDecorator<TQuery, TResponse> : QueryHandlerDecorator<TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
{
    private readonly ICacheStore<TQuery, TResponse> _cacheStore;
    private readonly ILogger<CachingQueryDecorator<TQuery, TResponse>> _logger;
    
    public CachingQueryDecorator(IHandler<TQuery, TResponse> handler, ICacheStore<TQuery, TResponse> cacheStore, ILogger<CachingQueryDecorator<TQuery, TResponse>> logger) : base(handler)
    {
        _cacheStore = cacheStore;
        _logger = logger;
    }

    public override async Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken)
    {
        var response = await _cacheStore.GetAsync(query, cancellationToken);

        if (response is not null)
        {
            _logger.LogInformation("Data has been exists in cache.");
            return Result<TResponse>.Success(response);
        }
        
        _logger.LogInformation("Data not exists in cache. Loading from persistence ...");

        var result = await Handler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
        {
            _logger.LogInformation("Loading failed.");
            return Result<TResponse>.Failure(result.Error!);
        }
        
        _logger.LogInformation("Data loaded from persistence. Loading to cache.");

        await _cacheStore.StoreAsync(query, result.Value!, cancellationToken);
        
        _logger.LogInformation("Data loaded to cache.");
        
        return Result<TResponse>.Success(result.Value!);
    }
}