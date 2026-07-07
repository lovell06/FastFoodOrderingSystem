using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache;

public interface IRefreshTokenStore
{
    Task<bool> SaveAsync(RefreshToken token, IDateTimeProvider clock, CancellationToken cancellationToken);
    Task<bool> RemoveByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}