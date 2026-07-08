using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh.Dtos;
using FastFoodOrderingSystem.Domain.RefreshTokens;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh;

public sealed class RefreshTokenHandler : IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
{
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly ILogger<IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>> _logger;
    private readonly IDateTimeProvider _clock;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenConfiguration _refreshTokenConfiguration;
    private readonly IAccessTokenConfiguration _accessTokenConfiguration;
    public RefreshTokenHandler(
        IRefreshTokenStore refreshTokenStore, 
        ILogger<IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>> logger, 
        IDateTimeProvider dateTimeProvider, 
        IAccessTokenProvider accessTokenProvider, 
        IUserRepository userRepository, 
        IRefreshTokenGenerator refreshTokenGenerator, 
        IRefreshTokenConfiguration refreshTokenConfiguration, 
        IAccessTokenConfiguration accessTokenConfiguration)
    {
        _refreshTokenStore = refreshTokenStore;
        _logger = logger;
        _clock = dateTimeProvider;
        _accessTokenProvider = accessTokenProvider;
        _userRepository = userRepository;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenConfiguration = refreshTokenConfiguration;
        _accessTokenConfiguration = accessTokenConfiguration;
    }

    public async Task<Result<RefreshTokenResponse>> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var token = Token.Create(command.RefreshToken);

        var tokenId = TokenId.Create(token);

        var result = await _refreshTokenStore.GetByIdAsync(tokenId, cancellationToken);

        if (result is null)
        {
            _logger.LogError($"Refresh failed. Refresh token with id: {tokenId} was been revoked. Occured at: {now}");
            return Result<RefreshTokenResponse>.Failure(RefreshTokenError.Failure);
        }

        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
        {
            _logger.LogError($"Refresh failed. User with id: {command.UserId} not found. Occured at: {now}");
            return Result<RefreshTokenResponse>.Failure(RefreshTokenError.Failure);
        }

        _logger.LogInformation(
            await _refreshTokenStore.RemoveByIdAsync(tokenId, cancellationToken) ?
                $"Revoke successful. Old refresh token with id: {tokenId.Value} was been revoked. Occurred at {now}" :
                $"Revoke failed. Old refresh token with id: {tokenId.Value} cannot revoke or not found. Occured at {now}");

        var accessToken = _accessTokenProvider.Generate(user);
        var refreshToken = RefreshToken.Create(
            userId: user.Id,
            token: _refreshTokenGenerator.Generate(),
            expiresAt: now.AddDays(_refreshTokenConfiguration.ExpireDays));

        if (!await _refreshTokenStore.SaveAsync(refreshToken, _clock, cancellationToken))
            throw new InvalidOperationException($"Cannot store new refresh token with id: {tokenId.Value}");
        
        _logger.LogInformation($"Store successful. Store new refresh token successful. Ocurred at: {now}");
        return Result<RefreshTokenResponse>.Success(new(
            new AccessTokenDto(accessToken, now.AddMinutes(_accessTokenConfiguration.ExpireMinutes)),
            new RefreshTokenDto(refreshToken.Token.Value, refreshToken.ExpiresAt)));
    }
}