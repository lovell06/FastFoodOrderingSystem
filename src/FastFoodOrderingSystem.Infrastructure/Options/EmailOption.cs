namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class EmailOption
{
    public const string SectionName = "EmailOption";
    public required string DisplayName { get; init; } 
    public required string UserName { get; init; } 
    public required string Password { get; init; } 
    public required string Host { get; init; } 
    public int Port { get; init; }
}