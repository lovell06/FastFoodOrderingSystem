using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;
using DomainValueObjects = FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;

public sealed class RefreshTokenMapper
{
    public static DomainValueObjects.RefreshToken ToValueObject(RefreshTokenSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot, "RefreshTokenSnapshot is null.");
        
        return DomainValueObjects.RefreshToken.Create(
            userId: snapshot.UserId,
            token: snapshot.Token,
            expiresAt: snapshot.ExpiresAt);
    }
}