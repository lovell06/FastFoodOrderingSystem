using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IPasswordGenerator
{
    Password Generate();
}