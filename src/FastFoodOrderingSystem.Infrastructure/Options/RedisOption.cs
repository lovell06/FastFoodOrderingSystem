namespace FastFoodOrderingSystem.Infrastructure.Options;

public record RedisOption(
    string ConnectionStrings,
    string InstanceName)
{
    public const string SectionName = "RedisOption";
}