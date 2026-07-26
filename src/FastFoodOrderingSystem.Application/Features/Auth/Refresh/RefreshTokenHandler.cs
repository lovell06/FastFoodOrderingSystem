using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh.Dtos;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh;

public sealed class RefreshTokenHandler(
    IRefreshTokenStore refreshTokenStore,
    ILogger<RefreshTokenHandler> logger,
    IDateTimeProvider dateTimeProvider,
    IAccessTokenProvider accessTokenProvider,
    IUserRepository userRepository,
    IRefreshTokenGenerator refreshTokenGenerator,
    IRefreshTokenConfiguration refreshTokenConfiguration,
    IAccessTokenConfiguration accessTokenConfiguration)
    : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<Result<RefreshTokenResponse>> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var now = dateTimeProvider.UtcNow;

        var result = await refreshTokenStore.GetAsync(
            userId: command.UserId,
            token: command.RefreshToken, 
            cancellationToken: cancellationToken);

        if (result is null)
        {
            logger.LogError($"Refresh failed. Refresh token with id: {command.RefreshToken} was been revoked. Occured at: {now}");
            return Result<RefreshTokenResponse>.Failure(RefreshTokenError.Unauthorized);
        }

        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
        {
            logger.LogError($"Refresh failed. User with id: {command.UserId} not found. Occured at: {now}");
            return Result<RefreshTokenResponse>.Failure(RefreshTokenError.Unauthorized);
        }

        logger.LogInformation(
            await refreshTokenStore.RevokeAsync(
                userId: command.UserId,
                token: command.RefreshToken, 
                cancellationToken: cancellationToken) ? 
                $"Revoke successful. Old refresh token with id: {command.RefreshToken} was been revoked. Occurred at {now}" :
                $"Revoke failed. Old refresh token with id: {command.RefreshToken} cannot revoke or not found. Occured at {now}");

        var accessToken = accessTokenProvider.Generate(user);
        var refreshToken = RefreshToken.Create(
            userId: user.Id,
            token: refreshTokenGenerator.Generate(),
            expiresAt: now.AddDays(refreshTokenConfiguration.ExpireDays));

        await refreshTokenStore.StoreAsync(
            userId: command.UserId,
            token: refreshToken, 
            clock: dateTimeProvider, 
            cancellationToken: cancellationToken);
        
        logger.LogInformation($"Store successful. Store new refresh token successful. Occurred at: {now}");
        
        return Result<RefreshTokenResponse>.Success(new RefreshTokenResponse(
            new AccessTokenDto(accessToken, now.AddMinutes(accessTokenConfiguration.ExpireMinutes)),
            new RefreshTokenDto(refreshToken.Token, refreshToken.ExpiresAt)));
    }
}