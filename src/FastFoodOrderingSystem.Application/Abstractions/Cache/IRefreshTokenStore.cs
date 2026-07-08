using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.RefreshTokens;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache;

public interface IRefreshTokenStore
{
    Task<bool> SaveAsync(RefreshToken refreshToken, IDateTimeProvider clock, CancellationToken cancellationToken);
    Task<bool> RemoveByIdAsync(TokenId id, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByIdAsync(TokenId id, CancellationToken cancellationToken);
}