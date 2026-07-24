using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;

public sealed record UpdateProfileCommand(
    Guid UserId,
    string? FullName,
    string? PhoneNumber) : ICommand<Unit>;