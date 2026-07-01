using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}