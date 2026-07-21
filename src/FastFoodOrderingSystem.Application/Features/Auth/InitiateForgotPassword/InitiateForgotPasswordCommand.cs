using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;

public record InitiateForgotPasswordCommand(string Email) : ICommand;