using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Emails;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Errors;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public sealed class RegisterHandler : ICommandHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPendingRegistrationStore _pendingRegistrationStore;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IOtpService _otpService;
    private readonly IOtpHashService _otpHashService;
    private readonly IOtpConfiguration _otpConfiguration;
    private readonly IEmailConfiguration _emailConfiguration;
    private readonly ILogger<RegisterHandler> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IDateTimeProvider _clock;

    public RegisterHandler(
        IUserRepository userRepository,
        IPendingRegistrationStore pendingRegistrationStore,
        IPasswordHashService passwordHashService,
        IOtpService otpService,
        IOtpHashService otpHashService,
        IOtpConfiguration otpConfiguration,
        ILogger<RegisterHandler> logger,
        IEmailSender emailSender,
        IEmailConfiguration emailConfiguration,
        IDateTimeProvider clock)
    {
        _userRepository = userRepository;
        _pendingRegistrationStore = pendingRegistrationStore;
        _passwordHashService = passwordHashService;
        _otpService = otpService;
        _otpHashService = otpHashService;
        _otpConfiguration = otpConfiguration;
        _logger = logger;
        _emailSender = emailSender;
        _emailConfiguration = emailConfiguration;
        _clock = clock;
    }

    public async Task<Result<RegisterResponse>> HandleAsync(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error!.Code, emailResult.Error.Message);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at: {now}");
            return Result<RegisterResponse>.Failure(err);
        }

        if (await _userRepository.EmailAlreadyExistedAsync(emailResult.Value!))
        {
            var err = RegisterError.EmailAlreadyExisted(emailResult.Value!);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at: {now}.");
            return Result<RegisterResponse>.Failure(err);
        }

        var fullNameResult = FullName.Create(command.FullName);
        if (fullNameResult.IsFailure)
        {
            var err = Error.Validation(fullNameResult.Error!.Code, fullNameResult.Error.Message);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at: {now}");
            return Result<RegisterResponse>.Failure(err);
        }

        var passwordResult = Password.Create(command.Password);
        if (passwordResult.IsFailure)
        {
            var err = Error.Validation(passwordResult.Error!.Code, passwordResult.Error.Message);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at: {now}");
            return Result<RegisterResponse>.Failure(err);
        }

        var passwordHash = _passwordHashService.Hash(null!, passwordResult.Value!);
        var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
        if (phoneNumberResult.IsFailure)
        {
            var err = Error.Validation(phoneNumberResult.Error!.Code, phoneNumberResult.Error.Message);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at: {now}");
            return Result<RegisterResponse>.Failure(err);
        }

        FullName fullName = fullNameResult.Value!;
        Email email = emailResult.Value!;
        PhoneNumber phoneNumber = phoneNumberResult.Value!;

        var otpCode = _otpService.GenerateCode(_otpConfiguration.Length);
        var otpCodeHash = _otpHashService.Hash(otpCode);
        var pending = PendingRegistration.Create(
            fullName: fullName,
            email: email,
            passwordHash: passwordHash,
            phone: phoneNumber,
            otpCodeHash: otpCodeHash,
            expiresAt: now.AddMinutes(_otpConfiguration.Expiration)
        );

        if (!await _pendingRegistrationStore.SaveAsync(pendingRegistration: pending, _clock,
                cancellationToken: cancellationToken))
        {
            _logger.LogError($"Pending registration store {email.Value} failed {now}.");
            return Result<RegisterResponse>.Failure(SystemError.Unexpected);
        }

        _logger.LogInformation($"Pending registration store {email.Value} successful {now}.");

        var emailContentResult = EmailContent.Create(
            from: _emailConfiguration.SenderAddress,
            to: email.Value,
            "Verify email",
            $"This is OTP verification code: {otpCode.Value}. Expiration after {_otpConfiguration.Expiration} minutes.");

        if (emailContentResult.IsFailure)
        {
            _logger.LogError($"Create EmailContent failed {now}.");
            return Result<RegisterResponse>.Failure(SystemError.Unexpected);
        }

        EmailContent emailContent = emailContentResult.Value!;

        await _emailSender.SendAsync(emailContent);
        _logger.LogInformation(
            $"Send OTP code from {emailContent.From.Value} to {emailContent.To.Value} successful {now}.");
        return Result<RegisterResponse>.Success(
            new RegisterResponse($"OTP code sent to email {emailContent.To.Value}."));
    }
}