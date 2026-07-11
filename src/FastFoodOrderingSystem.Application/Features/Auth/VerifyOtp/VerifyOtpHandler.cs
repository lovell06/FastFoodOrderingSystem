using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;

public sealed class VerifyOtpHandler : ICommandHandler<VerifyOtpCommand, Result<Unit>>
{
    private readonly IPendingRegistrationStore _pendingRegistrationStore;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<VerifyOtpHandler> _logger;
    private readonly IOtpHashService _optHashService;
    private readonly IUnitWork _unitWork;
    private readonly IDateTimeProvider _clock;

    public VerifyOtpHandler(
        IPendingRegistrationStore pendingRegistrationStore,
        IUserRepository userRepository,
        ILogger<VerifyOtpHandler> logger,
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
        VerifyOtpCommand command,
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

        var pending = await _pendingRegistrationStore.GetByEmailAsync(email);
        if (pending is null)
        {
            _logger.LogError(
                $"Verify OTP Failed. Email not found. Email: {email.Value}. At {now}.");
            return Result<Unit>.Failure(VerifyOtpError.AuthOtpInvalid);
        }

        if (!_optHashService.Verify(otpCode, pending.OtpCodeHash))
        {
            _logger.LogError(
                $"Verify OTP Failed. OTP code invalid. Email: {email.Value}. At {now}");
            return Result<Unit>.Failure(VerifyOtpError.AuthOtpInvalid);
        }

        if (pending.IsExpired(now))
        {
            _logger.LogError(
                $"Verify OTP Failed. OTP expired. Email: {email.Value}. At {now}.");
            return Result<Unit>.Failure(VerifyOtpError.AuthOtpInvalid);
        }

        _logger.LogInformation(
            $"Verify OTP Successful. Email: {email.Value}. At {now}");

        var user = User.CreateFromPending(pending, now);
        await _userRepository.InsertAsync(user, cancellationToken);
        await _unitWork.SaveChangeAsync(cancellationToken);

        await _pendingRegistrationStore.RemoveAsync(email, cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}