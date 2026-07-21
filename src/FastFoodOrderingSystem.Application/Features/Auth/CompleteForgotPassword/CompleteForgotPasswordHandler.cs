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

public class CompleteForgotPasswordHandler : IHandler<CompleteForgotPasswordCommand, Result<Unit>>
{
    private readonly IForgotPasswordOtpStore _forgotPasswordOtpStore;
    private readonly ILogger<CompleteForgotPasswordHandler> _logger;
    private readonly IDateTimeProvider _clock;
    private readonly IOtpHashService _otpHashService;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IEmailConfiguration _emailConfiguration;
    private readonly IEmailSender _emailSender;
    private readonly IUserRepository _userRepository;
    public CompleteForgotPasswordHandler(
        IForgotPasswordOtpStore forgotPasswordOtpStore, 
        ILogger<CompleteForgotPasswordHandler> logger, 
        IDateTimeProvider clock, 
        IOtpHashService otpHashService, 
        IPasswordGenerator passwordGenerator,
        IPasswordHashService passwordHashService,
        IEmailConfiguration emailConfiguration, 
        IEmailSender emailSender, 
        IUserRepository userRepository)
    {
        _forgotPasswordOtpStore = forgotPasswordOtpStore;
        _logger = logger;
        _clock = clock;
        _otpHashService = otpHashService;
        _passwordGenerator = passwordGenerator;
        _passwordHashService = passwordHashService;
        _emailConfiguration = emailConfiguration;
        _emailSender = emailSender;
        _userRepository = userRepository;
    }
    public async Task<Result<Unit>> HandleAsync(CompleteForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error!.Code, emailResult.Error.Message);
            _logger.LogError($"Code: {err.Code}. Message: {err.Message}. Occured at: {now}");
            return Result<Unit>.Failure(err);
        }

        var otpCodeResult = OtpCode.Create(command.OtpCode);
        if (otpCodeResult.IsFailure)
        {
            var err = Error.Validation(otpCodeResult.Error!.Code, otpCodeResult.Error.Message);
            _logger.LogError($"Code: {err.Code}. Message: {err.Message}. Occured at: {now}");
            return Result<Unit>.Failure(err);
        }

        Email email = emailResult.Value!;
        OtpCode otpCode = otpCodeResult.Value!;

        var forgotPasswordOtp = await _forgotPasswordOtpStore.GetByEmailAsync(email, cancellationToken);

        if (forgotPasswordOtp is null)
        {
            _logger.LogError(
                $"Email not found. Email: {email.Value} not found in OTP storage area. Occured at: {now}");
            return Result<Unit>.Failure(InitiateForgotPasswordError.Unauthorized);
        }

        if (!_otpHashService.Verify(otpCode, forgotPasswordOtp.CodeHash))
        {
            _logger.LogError(
                $"Verify OTP failed. OTP code: {otpCode.Value} incorrect. Occured at: {now}");
            return Result<Unit>.Failure(InitiateForgotPasswordError.Unauthorized);
        }

        _logger.LogInformation($"Verify OTP successful. Occured at: {now}");

        await _forgotPasswordOtpStore.RemoveByEmailAsync(email, cancellationToken);

        _logger.LogInformation($"Forgot password OTP was been remove in Storage area. Occured at: {now}");

        var user = await _userRepository.GetWithPasswordHistoriesByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            _logger.LogInformation($"User with email: {email.Value} not found. Occured at: {now}");
            return Result<Unit>.Failure(InitiateForgotPasswordError.Unauthorized);
        }

        var randomPassword = _passwordGenerator.Generate();
        var randomPasswordHash = _passwordHashService.Hash(user, randomPassword);

        if (user.ChangePasswordHash(randomPasswordHash).IsFailure)
            throw new InvalidOperationException("Cannot save temporary password.");
        
        _logger.LogInformation($"Change password successful. Occured at: {now}");

        var senderAddressResult = Email.Create(_emailConfiguration.SenderAddress);
        if (senderAddressResult.IsFailure)
            throw new InvalidOperationException(
                $"Code: {senderAddressResult.Error?.Code}. Message: {senderAddressResult.Error?.Message}. Email sender address invalid.");

        var emailContent = EmailContent.Create(
            senderAddressResult.Value!,
            email,
            "Provide new password for forgot password",
            $"This is temporary password by system's password generator. Please change new password after completed. \nPassword: {randomPassword.Value}");

        await _emailSender.SendAsync(emailContent);
        _logger.LogInformation($"The temporary password was been send to {email.Value}. Occured at: {now}");

        return Result<Unit>.Success(Unit.Value);
    }
}