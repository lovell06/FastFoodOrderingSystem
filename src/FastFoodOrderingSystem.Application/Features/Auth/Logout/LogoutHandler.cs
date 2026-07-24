using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Logout;

public sealed class LogoutHandler(
    IRefreshTokenStore refreshTokenStore,
    ILogger<LogoutHandler> logger,
    IDateTimeProvider clock)
    : ICommandHandler<LogoutCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(LogoutCommand command, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;

        var isSuccess = await refreshTokenStore.RevokeAsync(
            command.Token, 
            cancellationToken);

        logger.LogInformation(isSuccess
            ? $"Refresh token with id: {command.Token} has been revoked. Occured at: {now}" 
            : $"Refresh token with id: {command.Token} was already revoked.");

        return Result<Unit>.Success(Unit.Value);
    }
}