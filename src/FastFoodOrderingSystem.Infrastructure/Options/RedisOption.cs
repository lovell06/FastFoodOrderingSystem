namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class RedisOption
{
    public const string SectionName = "RedisOption";
    public string ConnectionStrings { get; }
    public string InstanceName { get; }
}