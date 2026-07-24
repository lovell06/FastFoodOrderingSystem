using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;

namespace FastFoodOrderingSystem.Api.Contracts.Users;

public record UpdateProfileRequest(
    Guid UserId,
    string? FullName,
    string? PhoneNumber)
{
    public UpdateProfileCommand ToCommand()
    {
        return new UpdateProfileCommand(UserId, FullName, PhoneNumber);
    }
}