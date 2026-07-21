using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;

public static class RefreshTokenMapper
{
    public static Application.Abstractions.Cache.RefreshToken.RefreshToken ToEntity(RefreshTokenSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot, "RefreshTokenSnapshot is null.");

        return Application.Abstractions.Cache.RefreshToken.RefreshToken.Create(
            userId: snapshot.UserId,
            token: snapshot.Token,
            expiresAt: snapshot.ExpiresAt);
    }

    public static RefreshTokenSnapshot ToSnapshot(Application.Abstractions.Cache.RefreshToken.RefreshToken refreshToken)
    {
        return new RefreshTokenSnapshot(
            UserId: refreshToken.UserId,
            Token: refreshToken.Token,
            ExpiresAt: refreshToken.ExpiresAt);
    }
}