using FastFoodOrderingSystem.Domain.Common.Enums;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface ICurrentUser
{
    Guid Id { get; }
    UserRole Role { get; }
}