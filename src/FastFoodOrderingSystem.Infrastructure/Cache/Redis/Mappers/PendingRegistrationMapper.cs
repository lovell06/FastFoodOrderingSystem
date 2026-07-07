using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;

public class PendingRegistrationMapper
{
    public static Domain.Users.PendingRegistration ToEntity(PendingRegistrationSnapshot snapshot)
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
        
        return Domain.Users.PendingRegistration.Create(
            fullName: fullNameResult.Value!,
            email: emailResult.Value!,
            passwordHash: passwordHashResult.Value!,
            phone: phoneNumberResult.Value!,
            otpCodeHash: otpCodeHashResult.Value!,
            expiresAt: expiresAt);
    }
}