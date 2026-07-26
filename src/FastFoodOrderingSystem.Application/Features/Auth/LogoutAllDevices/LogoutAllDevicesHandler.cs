using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.LogoutAllDevices;

public sealed class LogoutAllDevicesHandler(
    IRefreshTokenStore refreshTokenStore,
    ILogger<LogoutAllDevicesHandler> logger) : ICommandHandler<LogoutAllDevicesCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(LogoutAllDevicesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Revoking ...");
        var count = await refreshTokenStore.RevokeByUserAsync(
            userId: command.UserId,
            cancellationToken: cancellationToken);
        logger.LogInformation("Revoked {0} devices.", count);
        
        return Result<Unit>.Success(Unit.Value);
    }
}