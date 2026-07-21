using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Auth.Logout;

public sealed record LogoutCommand(string Token) : ICommand;