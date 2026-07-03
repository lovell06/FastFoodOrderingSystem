namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class EmailOption
{
    public const string SectionName = "EmailOption";
    public string DisplayName { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Host { get; init; } =  string.Empty;
    public int Port { get; init; }
}