using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.RefreshTokens;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Logout;

public sealed class LogoutHandler : ICommandHandler<LogoutCommand, Result<LogoutResponse>>
{
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly ILogger<ICommandHandler<LogoutCommand, Result<LogoutResponse>>> _logger;
    private readonly IDateTimeProvider _clock;
    public LogoutHandler(IRefreshTokenStore refreshTokenStore, ILogger<ICommandHandler<LogoutCommand, Result<LogoutResponse>>> logger, IDateTimeProvider clock)
    {
        _refreshTokenStore = refreshTokenStore;
        _logger = logger;
        _clock = clock;
    }

    public async Task<Result<LogoutResponse>> HandleAsync(LogoutCommand command, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var token = Token.Create(command.Token);

        var tokenId = TokenId.Create(token);

        var isSuccess = await _refreshTokenStore.RemoveByIdAsync(
            tokenId, 
            cancellationToken);

        _logger.LogInformation(!isSuccess
            ? $"Refresh token with id: {tokenId.Value} was already revoked."
            : $"Refresh token with id: {tokenId.Value} has been revoked. Occured at: {now}");

        return Result<LogoutResponse>.Success(new ());
    }
}