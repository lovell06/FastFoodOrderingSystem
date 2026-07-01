using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IPasswordHashService
{
    PasswordHash Hash(Password password);
    bool Verify(Password password, PasswordHash passwordHash);
}