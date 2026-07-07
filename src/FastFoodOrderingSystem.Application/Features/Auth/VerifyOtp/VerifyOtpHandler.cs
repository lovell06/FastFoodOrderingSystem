using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;

public sealed class VerifyOtpHandler : ICommandHandler<VerifyOtpCommand, Result<VerifyOtpResponse>>
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

    public async Task<Result<VerifyOtpResponse>> HandleAsync(
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
            return Result<VerifyOtpResponse>.Failure(err);
        }
        if (otpCodeResult.IsFailure)
        {
            var err = Error.Validation(otpCodeResult.Error!.Code, otpCodeResult.Error.Message);
            _logger.LogError($"Code: {err.Code} | Message: {err.Message} | Occured at {now}");
            return Result<VerifyOtpResponse>.Failure(err);
        }

        Email email = emailResult.Value!;
        OtpCode otpCode = otpCodeResult.Value!;

        var pending = await _pendingRegistrationStore.GetAsync(email);
        if (pending is null)
        {
            _logger.LogError(
                $"Verify OTP Failed. Email not found. Email: {email.Value}. At {now}.");
            return Result<VerifyOtpResponse>.Failure(VerifyOtpError.AuthOtpInvalid);
        }

        if (!_optHashService.Verify(otpCode, pending.OtpCodeHash))
        {
            _logger.LogError(
                $"Verify OTP Failed. OTP code invalid. Email: {email.Value}. At {now}");
            return Result<VerifyOtpResponse>.Failure(VerifyOtpError.AuthOtpInvalid);
        }

        if (pending.IsExpired(now))
        {
            _logger.LogError(
                $"Verify OTP Failed. OTP expired. Email: {email.Value}. At {now}.");
            return Result<VerifyOtpResponse>.Failure(VerifyOtpError.AuthOtpInvalid);
        }

        _logger.LogInformation(
            $"Verify OTP Successful. Email: {email.Value}. At {now}");

        var user = User.CreateFromPending(pending, now);
        await _userRepository.InsertAsync(user);
        await _unitWork.SaveChangeAsync(cancellationToken);

        await _pendingRegistrationStore.RemoveAsync(email, cancellationToken);

        return Result<VerifyOtpResponse>.Success(
            new VerifyOtpResponse("Verify OTP Successful."));
    }
}