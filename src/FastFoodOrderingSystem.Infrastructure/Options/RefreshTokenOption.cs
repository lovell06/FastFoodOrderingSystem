namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class RefreshTokenOption
{
    public const string SectionName = "RefreshTokenOption";
    public int ExpireDays { get; init; }
}