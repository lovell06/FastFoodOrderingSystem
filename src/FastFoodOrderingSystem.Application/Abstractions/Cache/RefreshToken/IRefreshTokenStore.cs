using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.RefreshTokens;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;

public interface IRefreshTokenStore
{
    Task<bool> SaveAsync(Domain.RefreshTokens.RefreshToken refreshToken, IDateTimeProvider clock, CancellationToken cancellationToken);
    Task<bool> RemoveByIdAsync(TokenId id, CancellationToken cancellationToken);
    Task<Domain.RefreshTokens.RefreshToken?> GetByIdAsync(TokenId id, CancellationToken cancellationToken);
}