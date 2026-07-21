using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;

public interface IForgotPasswordOtpStore
{
    public Task<bool> SaveAsync(
        ForgotPasswordOtp forgotPasswordOtp,
        IDateTimeProvider clock,
        CancellationToken cancellationToken = default);

    public Task<ForgotPasswordOtp?> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken = default);

    public Task<bool> RemoveByEmailAsync(
        Email email,
        CancellationToken cancellationToken = default);
}