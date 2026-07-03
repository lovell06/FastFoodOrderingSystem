namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class EmailOption
{
    public const string SectionName = "EmailOption";
    public string DisplayName { get; }
    public string UserName { get; }
    public string Password { get; }
    public string Host { get; }
    public int Port { get; }
}