using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;

public sealed class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand, Unit>
{
    private readonly IDateTimeProvider _clock;
    private readonly ILogger<ChangePasswordHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public ChangePasswordHandler(
        IDateTimeProvider clock, 
        ILogger<ChangePasswordHandler> logger, 
        IUserRepository userRepository, 
        IPasswordHashService passwordHashService)
    {
        _clock = clock;
        _logger = logger;
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }
    public async Task<Result<Unit>> HandleAsync(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var oldPasswordResult = Password.Create(command.OldPassword);
        var newPasswordResult = Password.Create(command.NewPassword);

        if (oldPasswordResult.IsFailure)
        {
            var err = Error.Validation(oldPasswordResult.Error!.Code, oldPasswordResult.Error.Message);
            _logger.LogError($"Code: {err.Code}. Message: {err.Message}. Occured at: {now}");
            return Result<Unit>.Failure(err);
        }

        if (newPasswordResult.IsFailure)
        {
            var err = Error.Validation(newPasswordResult.Error!.Code, newPasswordResult.Error.Message);
            _logger.LogError($"Code: {err.Code}. Message: {err.Message}. Occured at: {now}");
            return Result<Unit>.Failure(err);
        }

        Password oldPassword = oldPasswordResult.Value!;
        Password newPassword = newPasswordResult.Value!;

        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            _logger.LogError($"User not found. User with id: {command.UserId} not found. Occured at: {now}.");
            return Result<Unit>.Failure(ChangePasswordError.Unauthorized);
        }

        _logger.LogInformation($"Load user successful. User with id: {user.Id} is Loaded. Occured at: {now}.");

        if (!_passwordHashService.Verify(user, oldPassword, user.PasswordHash))
        {
            _logger.LogError($"Veriy failed. Old password incorrected. Occured at: {now}.");
            return Result<Unit>.Failure(ChangePasswordError.Unauthorized);
        }

        _logger.LogInformation($"Verify successful. Old password correct. Occured at: {now}");

        var newPasswordHash = _passwordHashService.Hash(user, newPassword);

        var changePasswordResult = user.ChangePasswordHash(newPasswordHash);
        if (changePasswordResult.IsFailure)
        {
            var err = changePasswordResult.Error!;
            _logger.LogError($"Code: {err.Code}. Message: {err.Message}. Occured at: {now}.");
            return Result<Unit>.Failure(Error.Conflict(err.Code, err.Message));
        }

        _logger.LogInformation($"Change password successful. User with id: {user.Id} was been change password. Occured at: {now}.");

        return Result<Unit>.Success(Unit.Value);
    }
}