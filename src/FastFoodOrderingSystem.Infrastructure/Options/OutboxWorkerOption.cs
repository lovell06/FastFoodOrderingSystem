namespace FastFoodOrderingSystem.Infrastructure.Options;

public class OutboxWorkerOption
{
    public TimeSpan Interval { get; } = TimeSpan.FromMilliseconds(500);
}