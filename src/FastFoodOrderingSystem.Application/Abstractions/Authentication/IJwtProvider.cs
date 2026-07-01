namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string Generate();
}