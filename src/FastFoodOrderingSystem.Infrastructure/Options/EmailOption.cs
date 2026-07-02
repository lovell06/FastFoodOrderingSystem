namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed record EmailOption(
    string DisplayName,
    string UserName,
    string Password,
    string Host,
    int Port)
{
    public const string SectionName = "EmailOption";
}