namespace FastFoodOrderingSystem.Application.Abstractions.Configurations;

public interface IAccessTokenConfiguration
{
    public int ExpireMinutes { get; }
}