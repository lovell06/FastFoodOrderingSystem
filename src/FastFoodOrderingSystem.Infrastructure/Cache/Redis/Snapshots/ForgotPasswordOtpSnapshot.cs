namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

public record ForgotPasswordOtpSnapshot(
    string Email, string OtpCodeHash, DateTime ExpiresAt);