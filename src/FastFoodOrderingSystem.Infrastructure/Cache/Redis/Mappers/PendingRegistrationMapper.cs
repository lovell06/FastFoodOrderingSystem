using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;

public static class PendingRegistrationMapper
{
    public static Application.Abstractions.Cache.PendingRegistration.PendingRegistration ToEntity(PendingRegistrationSnapshot snapshot)
    {
        var fullNameResult = FullName.Create(snapshot.FullName);
        var emailResult = Email.Create(snapshot.Id);
        var passwordHashResult = PasswordHash.Create(snapshot.PasswordHash);
        var phoneNumberResult = PhoneNumber.Create(snapshot.PhoneNumber);
        var otpCodeHashResult = OtpCodeHash.Create(snapshot.OtpCodeHash);
        var expiresAt = snapshot.ExpiresAt;

        if (fullNameResult.IsFailure ||
            emailResult.IsFailure ||
            passwordHashResult.IsFailure ||
            passwordHashResult.IsFailure ||
            phoneNumberResult.IsFailure ||
            otpCodeHashResult.IsFailure)
            throw new ArgumentException("PendingRegistrationSnapshot invalid.");
        
        return Application.Abstractions.Cache.PendingRegistration.PendingRegistration.Create(
            fullName: fullNameResult.Value!,
            email: emailResult.Value!,
            passwordHash: passwordHashResult.Value!,
            phone: phoneNumberResult.Value!,
            otpCodeHash: otpCodeHashResult.Value!,
            expiresAt: expiresAt);
    }

    public static PendingRegistrationSnapshot ToSnapshot(Application.Abstractions.Cache.PendingRegistration.PendingRegistration pending)
    {
        return new PendingRegistrationSnapshot(
            Id: pending.Email.Value,
            FullName: pending.FullName.Value,
            PasswordHash: pending.PasswordHash.Value,
            PhoneNumber: pending.PhoneNumber.Value,
            OtpCodeHash: pending.OtpCodeHash.Value,
            ExpiresAt: pending.ExpiresAt,
            AttemptCount: pending.AttemptCount);
    }
}