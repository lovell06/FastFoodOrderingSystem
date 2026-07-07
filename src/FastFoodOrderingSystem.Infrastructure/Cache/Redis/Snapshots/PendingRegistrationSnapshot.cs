namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

public sealed record PendingRegistrationSnapshot(
    string FullName,
    string Id,
    string PasswordHash,
    string PhoneNumber,
    string Role,
    string OtpCodeHash,
    DateTime ExpiresAt,
    int AttemptCount);