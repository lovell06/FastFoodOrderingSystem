using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Emails;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.CompleteForgotPassword;

public class CompleteForgotPasswordHandler(
    IForgotPasswordOtpStore forgotPasswordOtpStore,
    ILogger<CompleteForgotPasswordHandler> logger,
    IDateTimeProvider clock,
    IOtpHashService otpHashService,
    IPasswordGenerator passwordGenerator,
    IPasswordHashService passwordHashService,
    IEmailConfiguration emailConfiguration,
    IEmailSender emailSender,
    IUserRepository userRepository)
    : ICommandHandler<CompleteForgotPasswordCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(CompleteForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error.Code, emailResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        var otpCodeResult = OtpCode.Create(command.OtpCode);
        if (otpCodeResult.IsFailure)
        {
            var err = Error.Validation(otpCodeResult.Error.Code, otpCodeResult.Error.Message);
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<Unit>.Failure(err);
        }

        Email email = emailResult.Value;
        OtpCode otpCode = otpCodeResult.Value;

        var forgotPasswordOtp = await forgotPasswordOtpStore.GetByEmailAsync(email, cancellationToken);

        if (forgotPasswordOtp is null)
        {
            logger.LogError(
                $"Email not found. Email: {email.Value} not found in OTP storage area. Occured at: {now}");
            return Result<Unit>.Failure(InitiateForgotPasswordError.Unauthorized);
        }

        if (!otpHashService.Verify(otpCode, forgotPasswordOtp.CodeHash))
        {
            logger.LogError(
                $"Verify OTP failed. OTP code: {otpCode.Value} incorrect. Occured at: {now}");
            return Result<Unit>.Failure(InitiateForgotPasswordError.Unauthorized);
        }

        logger.LogInformation($"Verify OTP successful. Occured at: {now}");

        await forgotPasswordOtpStore.RemoveByEmailAsync(email, cancellationToken);

        logger.LogInformation($"Forgot password OTP was been remove in Storage area. Occured at: {now}");

        var user = await userRepository.GetWithPasswordHistoriesByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            logger.LogInformation($"User with email: {email.Value} not found. Occured at: {now}");
            return Result<Unit>.Failure(InitiateForgotPasswordError.Unauthorized);
        }

        var randomPassword = passwordGenerator.Generate();
        var randomPasswordHash = passwordHashService.Hash(user, randomPassword);

        if (user.ChangePasswordHash(randomPasswordHash).IsFailure)
            throw new InvalidOperationException("Cannot save temporary password.");
        
        logger.LogInformation($"Change password successful. Occured at: {now}");

        var senderAddressResult = Email.Create(emailConfiguration.SenderAddress);
        if (senderAddressResult.IsFailure)
            throw new InvalidOperationException(
                $"{senderAddressResult.Error.GetType().Name}. {senderAddressResult.Error.Code}. {senderAddressResult.Error.Message}. Email sender address invalid.");

        var emailContent = EmailContent.Create(
            senderAddressResult.Value,
            email,
            "Provide new password for forgot password",
            $"This is temporary password by system's password generator. Please change new password after completed. \nPassword: {randomPassword.Value}");

        await emailSender.SendAsync(emailContent);
        logger.LogInformation($"The temporary password was been send to {email.Value}. Occured at: {now}");

        return Result<Unit>.Success(Unit.Value);
    }
}