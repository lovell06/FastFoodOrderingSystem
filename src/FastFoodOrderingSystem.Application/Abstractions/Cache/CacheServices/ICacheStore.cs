namespace FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;

public interface ICacheStore<in TQuery, TData>
{
    Task<bool> StoreAsync(
        TQuery query, 
        TData data, 
        CancellationToken cancellationToken);
    Task<bool> RemoveAsync(
        TQuery query, 
        CancellationToken cancellationToken);
    Task<TData?> GetAsync(
        TQuery query, 
        CancellationToken cancellationToken);
}