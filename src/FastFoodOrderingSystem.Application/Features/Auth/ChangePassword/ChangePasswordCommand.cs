namespace FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;

public sealed record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword);