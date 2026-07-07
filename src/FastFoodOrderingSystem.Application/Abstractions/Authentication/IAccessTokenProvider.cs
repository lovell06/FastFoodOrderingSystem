using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IAccessTokenProvider
{
    string Generate(User user);
}