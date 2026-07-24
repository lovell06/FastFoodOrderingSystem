using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.PendingRegistration;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Customers.CompleteRegistration;

public sealed class CompleteRegistrationHandler(
    IPendingRegistrationStore pendingRegistrationStore,
    IUserRepository userRepository,
    ILogger<CompleteRegistrationHandler> logger,
    IOtpHashService optHashService,
    IDateTimeProvider clock)
    : ICommandHandler<CompleteRegistrationCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(
        CompleteRegistrationCommand command,
        CancellationToken cancellationToken)
    {
        DateTime now = clock.UtcNow;
        var emailResult = Email.Create(command.Email);
        var otpCodeResult = OtpCode.Create(command.Code);

        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error.Code, emailResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        if (otpCodeResult.IsFailure)
        {
            var err = Error.Validation(otpCodeResult.Error.Code, otpCodeResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        Email email = emailResult.Value;
        OtpCode otpCode = otpCodeResult.Value;

        var pending = await pendingRegistrationStore.GetByEmailAsync(email, cancellationToken);
        if (pending is null)
        {
            logger.LogError(
                $"Verify OTP Failed. Email not found. Email: {email.Value}. At {now}.");
            return Result<Unit>.Failure(CompleteRegistrationError.AuthOtpInvalid);
        }

        if (!optHashService.Verify(otpCode, pending.OtpCodeHash))
        {
            logger.LogError(
                $"Verify OTP Failed. OTP code invalid. Email: {email.Value}. At {now}");
            return Result<Unit>.Failure(CompleteRegistrationError.AuthOtpInvalid);
        }

        if (pending.IsExpired(now))
        {
            logger.LogError(
                $"Verify OTP Failed. OTP expired. Email: {email.Value}. At {now}.");
            return Result<Unit>.Failure(CompleteRegistrationError.AuthOtpInvalid);
        }

        logger.LogInformation(
            $"Verify OTP Successful. Email: {email.Value}. At {now}");

        var user = User.Register(
            fullName: pending.FullName,
            email: pending.Email,
            passwordHash: pending.PasswordHash,
            phoneNumber: pending.PhoneNumber,
            avatarImagePath: AvatarImagePath.Default(),
            now);
        
        await userRepository.InsertAsync(user, cancellationToken);

        await pendingRegistrationStore.RemoveAsync(email, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}