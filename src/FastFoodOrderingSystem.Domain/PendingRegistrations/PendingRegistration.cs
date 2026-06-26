using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.Enums;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Domain.PendingRegistrations;

public class PendingRegistration : AggregateRoot<int>
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public UserRole Role { get; private set; }

    protected PendingRegistration()
    {
    }

    private PendingRegistration(
        FullName fullName,
        Email email,
        PasswordHash passwordHash,
        PhoneNumber phone,
        UserRole role)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phone;
        Role = role;
    }

    public static PendingRegistration Create(
        FullName fullName,
        Email email,
        PasswordHash passwordHash,
        PhoneNumber phone,
        UserRole role)
    {
        return new(
            fullName: fullName,
            email: email,
            passwordHash: passwordHash,
            phone: phone,
            role: role);
    }

    public void ChangeFullName(FullName fullName)
    {
        FullName = fullName;
    }

    public void ChangePasswordHash(PasswordHash newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void ChangePhoneNumber(PhoneNumber phone)
    {
        PhoneNumber = phone;
    }

    public void ChangeRole(UserRole role)
    {
        Role = role;
    }
}