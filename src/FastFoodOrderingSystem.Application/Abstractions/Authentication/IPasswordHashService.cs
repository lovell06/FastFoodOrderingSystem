using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IPasswordHashService
{
    PasswordHash Hash(User user, Password password);
    bool Verify(User user, Password password, PasswordHash passwordHash);
}