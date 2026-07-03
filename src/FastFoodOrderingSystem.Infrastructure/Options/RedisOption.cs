namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class RedisOption
{
    public const string SectionName = "RedisOption";
    public required string ConnectionStrings { get; init; } 
    public required string InstanceName { get; init; } 
}