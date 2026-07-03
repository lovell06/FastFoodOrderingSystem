namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class EmailOption
{
    public const string SectionName = "EmailOption";
    public string DisplayName { get; init; } 
    public string Email { get; init; } 
    public string Password { get; init; } 
    public string Host { get; init; } 
    public int Port { get; init; }
}