using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Auth.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand;