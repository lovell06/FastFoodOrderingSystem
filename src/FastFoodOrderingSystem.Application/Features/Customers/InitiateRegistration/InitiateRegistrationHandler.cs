using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.PendingRegistration;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Emails;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Customers.InitiateRegistration;

public sealed class InitiateRegistrationHandler(
    IUserRepository userRepository,
    IPendingRegistrationStore pendingRegistrationStore,
    IPasswordHashService passwordHashService,
    IOtpService otpService,
    IOtpHashService otpHashService,
    IOtpConfiguration otpConfiguration,
    ILogger<InitiateRegistrationHandler> logger,
    IEmailSender emailSender,
    IEmailConfiguration emailConfiguration,
    IDateTimeProvider clock)
    : ICommandHandler<InitiateRegistrationCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(InitiateRegistrationCommand command,
        CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error.Code, emailResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}.{err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        if (await userRepository.EmailAlreadyExistedAsync(emailResult.Value, cancellationToken))
        {
            var err = InitiateRegistrationError.EmailAlreadyExisted(emailResult.Value);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        var fullNameResult = FullName.Create(command.FullName);
        if (fullNameResult.IsFailure)
        {
            var err = Error.Validation(fullNameResult.Error!.Code, fullNameResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        var passwordResult = Password.Create(command.Password);
        if (passwordResult.IsFailure)
        {
            var err = Error.Validation(passwordResult.Error.Code, passwordResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        var passwordHash = passwordHashService.Hash(null!, passwordResult.Value);
        var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
        if (phoneNumberResult.IsFailure)
        {
            var err = Error.Validation(phoneNumberResult.Error.Code, phoneNumberResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        var fullName = fullNameResult.Value;
        var email = emailResult.Value;
        var phoneNumber = phoneNumberResult.Value;

        var otpCode = otpService.GenerateCode(otpConfiguration.Length);
        var otpCodeHash = otpHashService.Hash(otpCode);
        var pending = PendingRegistration.Create(
            fullName: fullName,
            email: email,
            passwordHash: passwordHash,
            phone: phoneNumber,
            otpCodeHash: otpCodeHash,
            expiresAt: now.AddMinutes(otpConfiguration.Expiration)
        );

        await pendingRegistrationStore.SaveAsync(pendingRegistration: pending, clock,
            cancellationToken: cancellationToken);

        logger.LogInformation($"Pending registration store {email.Value} successful {now}.");

        var senderAddressResult = Email.Create(emailConfiguration.SenderAddress);
        if (senderAddressResult.IsFailure)
            throw new InvalidOperationException(
                $"{senderAddressResult.Error.Code}. {senderAddressResult.Error.Message}. Email sender address invalid.");
        
        var emailContent = EmailContent.Create(
            from: senderAddressResult.Value,
            to: email,
            "Verify email",
            $"This is OTP verification code: {otpCode.Value}. Expiration after {otpConfiguration.Expiration} minutes.");
        
        await emailSender.SendAsync(emailContent);
        logger.LogInformation(
            $"Send OTP code from {emailContent.From.Value} to {emailContent.To.Value} successful {now}.");
        return Result<Unit>.Success(Unit.Value);
    }
}