using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;

public sealed class ChangePasswordHandler(
    IDateTimeProvider clock,
    ILogger<ChangePasswordHandler> logger,
    IUserRepository userRepository,
    IPasswordHashService passwordHashService)
    : ICommandHandler<ChangePasswordCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;

        var oldPasswordResult = Password.Create(command.OldPassword);
        var newPasswordResult = Password.Create(command.NewPassword);

        if (oldPasswordResult.IsFailure)
        {
            var err = Error.Validation(oldPasswordResult.Error.Code, oldPasswordResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        if (newPasswordResult.IsFailure)
        {
            var err = Error.Validation(newPasswordResult.Error.Code, newPasswordResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        var oldPassword = oldPasswordResult.Value;
        var newPassword = newPasswordResult.Value;

        var user = await userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogError($"User not found. User with id: {command.UserId} not found. Occured at: {now}.");
            return Result<Unit>.Failure(ChangePasswordError.Unauthorized);
        }

        logger.LogInformation($"Load user successful. User with id: {user.Id} is Loaded. Occured at: {now}.");

        if (!passwordHashService.Verify(user, oldPassword, user.PasswordHash))
        {
            logger.LogError($"Verify failed. Old password incorrected. Occured at: {now}.");
            return Result<Unit>.Failure(ChangePasswordError.Unauthorized);
        }

        logger.LogInformation($"Verify successful. Old password correct. Occured at: {now}");

        var newPasswordHash = passwordHashService.Hash(user, newPassword);

        var changePasswordResult = user.ChangePasswordHash(newPasswordHash);
        if (changePasswordResult.IsFailure)
        {
            var err = changePasswordResult.Error;
            logger.LogError($"{err.GetType().Name}. {err.Code}. Message: {err.Message}. Occured at: {now}.");
            return Result<Unit>.Failure(Error.Conflict(err.Code, err.Message));
        }

        logger.LogInformation($"Change password successful. User with id: {user.Id} was been change password. Occured at: {now}.");

        return Result<Unit>.Success(Unit.Value);
    }
}