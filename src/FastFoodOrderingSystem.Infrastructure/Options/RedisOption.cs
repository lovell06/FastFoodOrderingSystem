namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class RedisOption
{
    public const string SectionName = "RedisOption";
    public string ConnectionStrings { get; init; } = string.Empty;
    public string InstanceName { get; init; } = string.Empty;
}