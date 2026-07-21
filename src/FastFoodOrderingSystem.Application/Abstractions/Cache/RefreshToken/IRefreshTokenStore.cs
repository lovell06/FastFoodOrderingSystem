using FastFoodOrderingSystem.Application.Abstractions.Time;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;

public interface IRefreshTokenStore
{
    Task<bool> StoreAsync(
        RefreshToken token,
        IDateTimeProvider clock,
        CancellationToken cancellationToken);
    Task<bool> RevokeAsync(
        string token, 
        CancellationToken cancellationToken);
    Task<RefreshToken?> GetAsync(
        string token, 
        CancellationToken cancellationToken);
}