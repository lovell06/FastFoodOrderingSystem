namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class RandomPasswordOption
{
    public const string SectionName = "RandomPasswordOption";
    public int Length { get; init; }
}