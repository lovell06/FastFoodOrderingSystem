using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.Enums;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Domain.Users;

public class PendingRegistration : Entity<Email>
{
    public FullName FullName { get; init; }
    public PasswordHash PasswordHash { get; init; }
    public PhoneNumber PhoneNumber { get; init; }
    public UserRole Role { get; init; }
    public OtpCodeHash OtpCodeHash { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public int AttemptCount { get; private set; }

    protected PendingRegistration()
    {
    }

    private PendingRegistration(
        Email email,
        PasswordHash passwordHash,
        FullName fullName,
        PhoneNumber phone,
        UserRole role,
        OtpCodeHash otpCodeHash,
        DateTime expiresAt,
        int attemptCount) : base(email)
    {
        FullName = fullName;
        PasswordHash = passwordHash;
        PhoneNumber = phone;
        Role = role;
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
            role: UserRole.Customer,
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