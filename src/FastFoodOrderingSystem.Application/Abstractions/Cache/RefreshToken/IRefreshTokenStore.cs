using FastFoodOrderingSystem.Application.Abstractions.Time;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;

public interface IRefreshTokenStore
{
    Task<bool> StoreAsync(
        Guid userId,
        RefreshToken token,
        IDateTimeProvider clock,
        CancellationToken cancellationToken);
    
    Task<bool> RevokeAsync(
        Guid userId,
        string token, 
        CancellationToken cancellationToken);

    Task<long> RevokeByUserAsync(
        Guid userId,
        CancellationToken cancellationToken);
    
    Task<RefreshToken?> GetAsync(
        Guid userId,
        string token, 
        CancellationToken cancellationToken);
}