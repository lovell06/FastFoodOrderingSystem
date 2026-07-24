namespace FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;

public interface ICacheStore<TData>
{
    Task<bool> StoreAsync(
        string key, 
        TData data, 
        TimeSpan ttl,
        CancellationToken cancellationToken);
    Task<bool> RemoveAsync(
        string key, 
        CancellationToken cancellationToken);
    Task<TData?> GetAsync(
        string key, 
        CancellationToken cancellationToken);
}