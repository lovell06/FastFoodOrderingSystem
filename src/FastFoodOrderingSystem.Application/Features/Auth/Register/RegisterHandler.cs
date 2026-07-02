using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Errors;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.Exceptions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public sealed class RegisterHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPendingRegistrationStore _pendingRegistrationStore;
    private readonly IUnitWork _unitWork;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IOtpService _otpService;
    private readonly IOtpHashService _otpHashService;
    private readonly IOtpConfiguration _otpConfiguration;
    public RegisterHandler(
        IUserRepository userRepository, 
        IPendingRegistrationStore pendingRegistrationStore,
        IUnitWork unitWork,
        IPasswordHashService passwordHashService,
        IOtpService otpService,
        IOtpHashService otpHashService,
        IOtpConfiguration otpConfiguration)
    {
        _userRepository = userRepository;
        _pendingRegistrationStore = pendingRegistrationStore;
        _unitWork = unitWork;
        _passwordHashService = passwordHashService;
        _otpService = otpService;
        _otpHashService = otpHashService;
        _otpConfiguration = otpConfiguration;
    }
    public async Task<Result<RegisterResponse>> HandleAsync(RegisterCommand command)
    {
        try
        {
            var email = Email.Create(command.Email);

            if (await _userRepository.EmailAlreadyExistedAsync(email))
                return Result<RegisterResponse>.Failure(RegisterError.EmailAlreadyExisted(email));

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
                expiresAt: DateTime.Now.AddMinutes(_otpConfiguration.Expiration)
            );

            await _pendingRegistrationStore.SaveAsync(pendingRegistration: pending);

            return Result<RegisterResponse>.Success(new RegisterResponse(""));
        }
        catch (InvalidEmailException exception)
        {
            return Result<RegisterResponse>.Failure(RegisterError.InvalidEmail(exception));
        }
        catch (InvalidFullNameException exception)
        {
            return Result<RegisterResponse>.Failure(RegisterError.InvalidFullName(exception));
        }
        catch (InvalidPasswordException exception)
        {
            return Result<RegisterResponse>.Failure(RegisterError.InvalidPassword(exception));
        }
        catch (InvalidPhoneNumberException exception)
        {
            return Result<RegisterResponse>.Failure(RegisterError.InvalidPhoneNumber(exception));
        }
        catch (InvalidOtpCodeException exception)
        {
            return Result<RegisterResponse>.Failure(RegisterError.InvalidOtpCode(exception));
        }
        catch (Exception exception)
        {
            return Result<RegisterResponse>.Failure(SystemError.Unexpected);
        }
    }
}