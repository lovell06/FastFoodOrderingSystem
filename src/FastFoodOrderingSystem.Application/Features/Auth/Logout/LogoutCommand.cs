using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Auth.Logout;

public sealed record LogoutCommand(Guid UserId, string Token) : ICommand<Unit>;