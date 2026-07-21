using ICommand = FastFoodOrderingSystem.Application.Common.Cqrs.ICommand;

namespace FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;

public sealed record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword) : ICommand;