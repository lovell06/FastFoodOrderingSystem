using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Errors;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;
using FastFoodOrderingSystem.Domain.Users;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public sealed class RegisterHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPendingRegistrationStore _pendingRegistrationStore;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IOtpService _otpService;
    private readonly IOtpHashService _otpHashService;
    private readonly IOtpConfiguration _otpConfiguration;
    private readonly ILogger<RegisterHandler> _logger;
    public RegisterHandler(
        IUserRepository userRepository, 
        IPendingRegistrationStore pendingRegistrationStore,
        IPasswordHashService passwordHashService,
        IOtpService otpService,
        IOtpHashService otpHashService,
        IOtpConfiguration otpConfiguration,
        ILogger<RegisterHandler> logger)
    {
        _userRepository = userRepository;
        _pendingRegistrationStore = pendingRegistrationStore;
        _passwordHashService = passwordHashService;
        _otpService = otpService;
        _otpHashService = otpHashService;
        _otpConfiguration = otpConfiguration;
        _logger = logger;
    }
    public async Task<Result<RegisterResponse>> HandleAsync(RegisterCommand command)
    {
        var now = DateTime.Now;
        try
        {
            var email = Email.Create(command.Email);

            if (await _userRepository.EmailAlreadyExistedAsync(email))
            {
                var err = RegisterError.EmailAlreadyExisted(email);
                _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at: {now}.");
                return Result<RegisterResponse>.Failure(err);
            }

            var fullName = FullName.Create(command.FullName);
            var password = Password.Create(command.Password);
            var passwordHash = _passwordHashService.Hash(default!, password);
            var phoneNumber = PhoneNumber.Create(command.PhoneNumber);
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

            if (!await _pendingRegistrationStore.SaveAsync(pendingRegistration: pending))
            {
                _logger.LogError($"Pending registration store {email.Value} failed {now}.");
            }

            _logger.LogInformation($"Pending registration store {email.Value} successful {now}.");
            return Result<RegisterResponse>.Success(new RegisterResponse($"OTP code sent to email {email.Value}."));
        }
        catch (InvalidEmailException exception)
        {
            _logger.LogError($"Code: {exception.Code} | Message: {exception.Message} | Occured at: {now}.");
            return Result<RegisterResponse>.Failure(RegisterError.InvalidEmail(exception));
        }
        catch (InvalidFullNameException exception)
        {
            _logger.LogError($"Code: {exception.Code} | Message: {exception.Message} | Occured at: {now}.");
            return Result<RegisterResponse>.Failure(RegisterError.InvalidFullName(exception));
        }
        catch (InvalidPasswordException exception)
        {
            _logger.LogError($"Code: {exception.Code} | Message: {exception.Message} | Occured at: {now}.");
            return Result<RegisterResponse>.Failure(RegisterError.InvalidPassword(exception));
        }
        catch (InvalidPhoneNumberException exception)
        {
            _logger.LogError($"Code: {exception.Code} | Message: {exception.Message} | Occured at: {now}.");
            return Result<RegisterResponse>.Failure(RegisterError.InvalidPhoneNumber(exception));
        }
        catch (InvalidOtpCodeException exception)
        {
            _logger.LogError($"Code: {exception.Code} | Message: {exception.Message} | Occured at: {now}.");
            return Result<RegisterResponse>.Failure(RegisterError.InvalidOtpCode(exception));
        }
        catch (Exception exception)
        {
            _logger.LogError($"Message: {exception.Message} | Occured at: {now}.");
            return Result<RegisterResponse>.Failure(SystemError.Unexpected);
        }
    }
}