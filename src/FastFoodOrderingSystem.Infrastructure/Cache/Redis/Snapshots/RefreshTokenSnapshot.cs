namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

public record RefreshTokenSnapshot(Guid Id, Guid UserId, string Token, DateTime ExpiresAt);