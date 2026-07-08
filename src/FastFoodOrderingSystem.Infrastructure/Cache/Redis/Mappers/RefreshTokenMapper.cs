using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;
using DomainAggregateRoot = FastFoodOrderingSystem.Domain.RefreshTokens;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;

public static class RefreshTokenMapper
{
    public static DomainAggregateRoot.RefreshToken ToEntity(RefreshTokenSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot, "RefreshTokenSnapshot is null.");

        var tokenId = DomainAggregateRoot.TokenId.Create(snapshot.Id);
        var token = DomainAggregateRoot.Token.Create(snapshot.Token);
        return DomainAggregateRoot.RefreshToken.Create(
            id: tokenId,
            userId: snapshot.UserId,
            token: token,
            expiresAt: snapshot.ExpiresAt);
    }

    public static RefreshTokenSnapshot ToSnapshot(DomainAggregateRoot.RefreshToken refreshToken)
    {
        return new RefreshTokenSnapshot(
            Id: refreshToken.Id.Value,
            UserId: refreshToken.UserId,
            Token: refreshToken.Token.Value,
            ExpiresAt: refreshToken.ExpiresAt);
    }
}