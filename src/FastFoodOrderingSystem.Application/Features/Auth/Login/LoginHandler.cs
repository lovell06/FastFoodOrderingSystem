using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Login.Dtos;
using FastFoodOrderingSystem.Domain.RefreshTokens;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Login;

public class LoginHandler : ICommandHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<LoginHandler> _logger;
    private readonly IDateTimeProvider _clock;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IRefreshTokenGenerator _refreshToken;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IRefreshTokenConfiguration _refreshTokenConfiguration;

    public LoginHandler(
        IUserRepository userRepository,
        ILogger<LoginHandler> logger,
        IDateTimeProvider dateTimeProvider,
        IPasswordHashService passwordHashService,
        IAccessTokenProvider jwtProvider,
        IRefreshTokenGenerator refreshToken,
        IRefreshTokenStore refreshTokenStore,
        IRefreshTokenConfiguration refreshTokenConfiguration)
    {
        _userRepository = userRepository;
        _logger = logger;
        _clock = dateTimeProvider;
        _passwordHashService = passwordHashService;
        _accessTokenProvider = jwtProvider;
        _refreshToken = refreshToken;
        _refreshTokenStore = refreshTokenStore;
        _refreshTokenConfiguration = refreshTokenConfiguration;
    }

    public async Task<Result<LoginResponse>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error!.Code, emailResult.Error.Message);
            _logger.LogError($"Code: {err.Code}. Message: {err.Message}. Occured at {now}");
            return Result<LoginResponse>.Failure(err);
        }

        var passwordResult = Password.Create(command.Password);
        if (passwordResult.IsFailure)
        {
            var err = Error.Validation(passwordResult.Error!.Code, passwordResult.Error.Message);
            _logger.LogError($"Code: {err.Code}. Message: {err.Message}. Occured at {now}");
            return Result<LoginResponse>.Failure(err);
        }

        Email email = emailResult.Value!;
        Password password = passwordResult.Value!;

        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            _logger.LogError($"Email not found. Email {email.Value} not existed. Occurred at: {now}");
            return Result<LoginResponse>.Failure(LoginError.Unauthorized);
        }

        if (!_passwordHashService.Verify(user, password, user.PasswordHash))
        {
            _logger.LogError($"Password incorrect. Occurred at: {now}");
            return Result<LoginResponse>.Failure(LoginError.Unauthorized);
        }

        var accessToken = _accessTokenProvider.Generate(user);

        var refreshToken = RefreshToken.Create(
            userId: user.Id,
            token: _refreshToken.Generate(),
            now.AddDays(_refreshTokenConfiguration.ExpireDays));

        await _refreshTokenStore.SaveAsync(refreshToken, _clock, cancellationToken);

        _logger.LogInformation($"Store refresh token successful. Ocurred at: {now}");

        var response = new LoginResponse(
            AccessToken: accessToken,
            RefreshTokenInfo: new RefreshTokenDto(
                UserId: refreshToken.UserId,
                Token: refreshToken.Token.Value,
                ExpiresAt: refreshToken.ExpiresAt),
            UserInfo: new UserDto(
                FullName: user.FullName.Value,
                Email: user.Email.Value,
                PhoneNumber: user.PhoneNumber.Value));

        _logger.LogInformation($"User {user.Email.Value} login succesful. {now}");
        return Result<LoginResponse>.Success(response);
    }
}