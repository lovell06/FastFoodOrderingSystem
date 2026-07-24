using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Emails;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Errors;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;

public sealed class InitiateForgotPasswordHandler(
    IUserRepository userRepository,
    IEmailSender emailSender,
    IOtpService otpService,
    IOtpHashService otpHashService,
    IForgotPasswordOtpStore forgotPasswordOtpStore,
    IDateTimeProvider clock,
    ILogger<InitiateForgotPasswordHandler> logger,
    IOtpConfiguration otpConfiguration,
    IEmailConfiguration emailConfiguration)
    : ICommandHandler<InitiateForgotPasswordCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(InitiateForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;

        var emailResult = Email.Create(command.Email);

        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error.Code, emailResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        Email email = emailResult.Value;

        if (!await userRepository.EmailAlreadyExistedAsync(email, cancellationToken))
        {
            logger.LogWarning($"User not found. Email is not exists in system. Occured at {now}");
            return Result<Unit>.Success(Unit.Value);
        }

        var otpCode = otpService.GenerateCode(otpConfiguration.Length);

        var otpCodeHash = otpHashService.Hash(otpCode);

        var forgotPasswordOtp = ForgotPasswordOtp.Create(
            email: email, 
            codeHash: otpCodeHash, 
            expriresAt: now.AddMinutes(otpConfiguration.Expiration));

        if (!await forgotPasswordOtpStore.SaveAsync(forgotPasswordOtp, clock, cancellationToken))
        {
            logger.LogError($"Store forgot password OTP for account {email.Value} failed. Occured at {now}");
            return Result<Unit>.Failure(SystemError.Unexpected);
        }

        logger.LogInformation($"Store forgot password OTP for account {email.Value} successful. Occured at {now}");

        var senderAddressResult = Email.Create(emailConfiguration.SenderAddress);
        if (senderAddressResult.IsFailure)
            throw new InvalidOperationException(
                $"Code: {senderAddressResult.Error.Code}. Message: {senderAddressResult.Error.Message}. Email sender address invalid.");
        
        var emailContent = EmailContent.Create(
            senderAddressResult.Value,
            email,
            "Forgot password",
            $"Forgot password OTP: {otpCode.Value}. Expiration: {otpConfiguration.Expiration}'.");

        await emailSender.SendAsync(emailContent);
        logger.LogInformation($"OTP was been sent to {email.Value}. Occured at {now}");

        return Result<Unit>.Success(Unit.Value);
    }
}