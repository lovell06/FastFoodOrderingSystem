using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IPasswordHashService
{
    PasswordHash Hash(User user, Password password);
    bool Verify(User user, Password password, PasswordHash passwordHash);
}