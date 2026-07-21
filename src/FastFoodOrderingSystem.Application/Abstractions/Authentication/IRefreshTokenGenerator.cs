namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IRefreshTokenGenerator
{
    string Generate();
}