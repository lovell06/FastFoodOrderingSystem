using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh;

public sealed record RefreshTokenCommand(Guid UserId, string RefreshToken) : ICommand;