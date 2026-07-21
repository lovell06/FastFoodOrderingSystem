namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class OutboxCleanupWorkerOption
{
    public TimeSpan Interval { get; } = TimeSpan.FromHours(12);
    public TimeSpan Retention { get; } = TimeSpan.FromDays(7);
}