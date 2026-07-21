using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Logout;

public sealed class LogoutHandler : ICommandHandler<LogoutCommand, Unit>
{
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly ILogger<LogoutHandler> _logger;
    private readonly IDateTimeProvider _clock;
    public LogoutHandler(IRefreshTokenStore refreshTokenStore, 
        ILogger<LogoutHandler> logger, 
        IDateTimeProvider clock)
    {
        _refreshTokenStore = refreshTokenStore;
        _logger = logger;
        _clock = clock;
    }

    public async Task<Result<Unit>> HandleAsync(LogoutCommand command, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var isSuccess = await _refreshTokenStore.RevokeAsync(
            command.Token, 
            cancellationToken);

        _logger.LogInformation(!isSuccess
            ? $"Refresh token with id: {command.Token} was already revoked."
            : $"Refresh token with id: {command.Token} has been revoked. Occured at: {now}");

        return Result<Unit>.Success(Unit.Value);
    }
}