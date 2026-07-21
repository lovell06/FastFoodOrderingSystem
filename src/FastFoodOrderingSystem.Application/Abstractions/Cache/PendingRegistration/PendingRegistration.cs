using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.Enums;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache.PendingRegistration;

public class PendingRegistration
{
    public Email Email { get; init; }
    public FullName FullName { get; init; }
    public PasswordHash PasswordHash { get; init; }
    public PhoneNumber PhoneNumber { get; init; }
    public OtpCodeHash OtpCodeHash { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public int AttemptCount { get; private set; }

    private PendingRegistration(
        Email email,
        PasswordHash passwordHash,
        FullName fullName,
        PhoneNumber phone,
        OtpCodeHash otpCodeHash,
        DateTime expiresAt,
        int attemptCount)
    {
        Email = email;
        FullName = fullName;
        PasswordHash = passwordHash;
        PhoneNumber = phone;
        OtpCodeHash = otpCodeHash;
        ExpiresAt = expiresAt;
        AttemptCount = attemptCount;
    }

    public static PendingRegistration Create(
        FullName fullName,
        Email email,
        PasswordHash passwordHash,
        PhoneNumber phone,
        OtpCodeHash otpCodeHash,
        DateTime expiresAt)
    {
        return new(
            fullName: fullName,
            email: email,
            passwordHash: passwordHash,
            phone: phone,
            otpCodeHash: otpCodeHash,
            expiresAt: expiresAt,
            attemptCount: 0);
    }

    public void ChangeOtpCodeHash(OtpCodeHash otpCodeHash, DateTime expiresAt)
    {
        OtpCodeHash = otpCodeHash;
        ExpiresAt = expiresAt;
        ResetAttempt();
    }

    private void ResetAttempt()
    {
        AttemptCount = 0;
    }

    public void IncreaseAttempt()
    {
        ++AttemptCount;
    }

    public bool IsExpired(DateTime now)
    {
        return now >= ExpiresAt;
    }
}