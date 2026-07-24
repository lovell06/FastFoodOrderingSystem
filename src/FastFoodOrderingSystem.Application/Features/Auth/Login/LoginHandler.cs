using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Login.Dtos;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Login;

public class LoginHandler(
    IUserRepository userRepository,
    ILogger<LoginHandler> logger,
    IDateTimeProvider dateTimeProvider,
    IPasswordHashService passwordHashService,
    IAccessTokenProvider jwtProvider,
    IRefreshTokenGenerator refreshTokenGenerator,
    IRefreshTokenStore refreshTokenStore,
    IRefreshTokenConfiguration refreshTokenConfiguration)
    : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var now = dateTimeProvider.UtcNow;

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error.Code, emailResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<LoginResponse>.Failure(err);
        }

        var passwordResult = Password.Create(command.Password);
        if (passwordResult.IsFailure)
        {
            var err = Error.Validation(passwordResult.Error.Code, passwordResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<LoginResponse>.Failure(err);
        }

        var email = emailResult.Value;
        var password = passwordResult.Value;

        var user = await userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            logger.LogError($"Email not found. Email {email.Value} not existed. Occurred at: {now}");
            return Result<LoginResponse>.Failure(LoginError.Unauthorized);
        }

        if (!passwordHashService.Verify(user, password, user.PasswordHash))
        {
            logger.LogError($"Password incorrect. Occurred at: {now}");
            return Result<LoginResponse>.Failure(LoginError.Unauthorized);
        }

        var accessToken = jwtProvider.Generate(user);

        var refreshToken = RefreshToken.Create(
            userId: user.Id,
            token: refreshTokenGenerator.Generate(),
            now.AddDays(refreshTokenConfiguration.ExpireDays));

        await refreshTokenStore.StoreAsync(
            refreshToken,
            dateTimeProvider,
            cancellationToken);

        logger.LogInformation($"Store refresh token successful. Occurred at: {now}");

        var response = new LoginResponse(
            AccessToken: accessToken,
            RefreshTokenInfo: new RefreshTokenDto(
                UserId: user.Id,
                Token: refreshToken.Token,
                ExpiresAt: refreshToken.ExpiresAt),
            UserInfo: new UserDto(
                FullName: user.FullName.Value,
                Email: user.Email.Value,
                PhoneNumber: user.PhoneNumber.Value));

        logger.LogInformation($"User {user.Email.Value} login successful. {now}");
        return Result<LoginResponse>.Success(response);
    }
}