using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache.PendingRegistration;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Customers.Register;

public sealed class VerifyRegisterHandler : ICommandHandler<VerifyRegisterCommand, Result<Unit>>
{
    private readonly IPendingRegistrationStore _pendingRegistrationStore;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<VerifyRegisterHandler> _logger;
    private readonly IOtpHashService _optHashService;
    private readonly IUnitWork _unitWork;
    private readonly IDateTimeProvider _clock;

    public VerifyRegisterHandler(
        IPendingRegistrationStore pendingRegistrationStore,
        IUserRepository userRepository,
        ILogger<VerifyRegisterHandler> logger,
        IOtpHashService optHashService,
        IUnitWork unitWork,
        IDateTimeProvider clock)
    {
        _pendingRegistrationStore = pendingRegistrationStore;
        _userRepository = userRepository;
        _logger = logger;
        _optHashService = optHashService;
        _unitWork = unitWork;
        _clock = clock;
    }

    public async Task<Result<Unit>> HandleAsync(
        VerifyRegisterCommand command,
        CancellationToken cancellationToken)
    {
        DateTime now = _clock.UtcNow;
        var emailResult = Email.Create(command.Email);
        var otpCodeResult = OtpCode.Create(command.Code);

        if (emailResult.IsFailure)
        {
            var err = Error.Validation(emailResult.Error!.Code, emailResult.Error.Message);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at {now}");
            return Result<Unit>.Failure(err);
        }

        if (otpCodeResult.IsFailure)
        {
            var err = Error.Validation(otpCodeResult.Error!.Code, otpCodeResult.Error.Message);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at {now}");
            return Result<Unit>.Failure(err);
        }

        Email email = emailResult.Value!;
        OtpCode otpCode = otpCodeResult.Value!;

        var pending = await _pendingRegistrationStore.GetByEmailAsync(email, cancellationToken);
        if (pending is null)
        {
            _logger.LogError(
                $"Verify OTP Failed. Email not found. Email: {email.Value}. At {now}.");
            return Result<Unit>.Failure(VerifyRegisterError.AuthOtpInvalid);
        }

        if (!_optHashService.Verify(otpCode, pending.OtpCodeHash))
        {
            _logger.LogError(
                $"Verify OTP Failed. OTP code invalid. Email: {email.Value}. At {now}");
            return Result<Unit>.Failure(VerifyRegisterError.AuthOtpInvalid);
        }

        if (pending.IsExpired(now))
        {
            _logger.LogError(
                $"Verify OTP Failed. OTP expired. Email: {email.Value}. At {now}.");
            return Result<Unit>.Failure(VerifyRegisterError.AuthOtpInvalid);
        }

        _logger.LogInformation(
            $"Verify OTP Successful. Email: {email.Value}. At {now}");

        var user = User.Create(
            fullName: pending.FullName,
            email: pending.Id,
            passwordHash: pending.PasswordHash,
            phoneNumber: pending.PhoneNumber,
            avatarImagePath: AvatarImagePath.Default(),
            role: pending.Role,
            now);
        await _userRepository.InsertAsync(user, cancellationToken);
        await _unitWork.SaveChangeAsync(cancellationToken);

        await _pendingRegistrationStore.RemoveAsync(email, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}