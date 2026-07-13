using FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record ChangePasswordRequest(string OldPassword, string NewPassword)
{
    public ChangePasswordCommand ToCommand(Guid userId)
    {
        return new ChangePasswordCommand(
            UserId: userId, 
            OldPassword: OldPassword, 
            NewPassword: NewPassword);
    }
}