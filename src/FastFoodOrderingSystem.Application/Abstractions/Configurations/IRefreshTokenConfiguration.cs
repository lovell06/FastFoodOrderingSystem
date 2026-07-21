namespace FastFoodOrderingSystem.Application.Abstractions.Configurations;

public interface IRefreshTokenConfiguration
{
    int ExpireDays { get; init; }
}