namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

public record RefreshTokenSnapshot(Guid UserId, string Token, DateTime ExpiresAt);