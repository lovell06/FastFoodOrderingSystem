namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class RedisOption
{
    public const string SectionName = "RedisOption";
    public string ConnectionStrings { get; init; } 
    public string InstanceName { get; init; } 
}