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

public sealed class InitiateForgotPasswordHandler : ICommandHandler<InitiateForgotPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;
    private readonly IOtpService _otpService;
    private readonly IOtpHashService _otpHashService;
    private readonly IForgotPasswordOtpStore _forgotPasswordOtpStore;
    private readonly IDateTimeProvider _clock;
    private readonly ILogger<InitiateForgotPasswordHandler> _logger;
    private readonly IOtpConfiguration _otpConfiguration;
    private readonly IEmailConfiguration _emailConfiguration;

    public InitiateForgotPasswordHandler(
        IUserRepository userRepository, 
        IEmailSender emailSender, 
        IOtpService otpService, 
        IOtpHashService otpHashService, 
        IForgotPasswordOtpStore forgotPasswordOtpStore, 
        IDateTimeProvider clock, 
        ILogger<InitiateForgotPasswordHandler> logger, 
        IOtpConfiguration otpConfiguration, 
        IEmailConfiguration emailConfiguration)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
        _otpService = otpService;
        _otpHashService = otpHashService;
        _forgotPasswordOtpStore = forgotPasswordOtpStore;
        _clock = clock;
        _logger = logger;
        _otpConfiguration = otpConfiguration;
        _emailConfiguration = emailConfiguration;
    }

    public async Task<Result<Unit>> HandleAsync(InitiateForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var emailResult = Email.Create(command.Email);

        if (emailResult.IsFailure)
        {
            var err = emailResult.Error;
            _logger.LogError($"Code: {err!.Code}. Message {err.Message}. Occured at: {now}");
            return Result<Unit>.Failure(Error.Validation(err.Code, err.Message));
        }

        Email email = emailResult.Value!;

        if (!await _userRepository.EmailAlreadyExistedAsync(email, cancellationToken))
        {
            _logger.LogWarning($"User not found. Email is not exists in system. Occured at {now}");
            return Result<Unit>.Success(Unit.Value);
        }

        var otpCode = _otpService.GenerateCode(_otpConfiguration.Length);

        var otpCodeHash = _otpHashService.Hash(otpCode);

        var forgotPasswordOtp = ForgotPasswordOtp.Create(
            email: email, 
            codeHash: otpCodeHash, 
            expriresAt: now.AddMinutes(_otpConfiguration.Expiration));

        if (!await _forgotPasswordOtpStore.SaveAsync(forgotPasswordOtp, _clock, cancellationToken))
        {
            _logger.LogError($"Store forgot password OTP for account {email.Value} failed. Occured at {now}");
            return Result<Unit>.Failure(SystemError.Unexpected);
        }

        _logger.LogInformation($"Store forgot password OTP for account {email.Value} successful. Occured at {now}");

        var senderAddressResult = Email.Create(_emailConfiguration.SenderAddress);
        if (senderAddressResult.IsFailure)
            throw new InvalidOperationException(
                $"Code: {senderAddressResult.Error?.Code}. Message: {senderAddressResult.Error?.Message}. Email sender address invalid.");
        
        var emailContent = EmailContent.Create(
            senderAddressResult.Value!,
            email,
            "Forgot password",
            $"Forgot password OTP: {otpCode.Value}. Expiration: {_otpConfiguration.Expiration}'.");

        await _emailSender.SendAsync(emailContent);
        _logger.LogInformation($"OTP was been sent to {email.Value}. Occured at {now}");

        return Result<Unit>.Success(Unit.Value);
    }
}