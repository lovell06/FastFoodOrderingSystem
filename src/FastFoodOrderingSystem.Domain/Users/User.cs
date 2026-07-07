using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.Enums;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users.Errors;

namespace FastFoodOrderingSystem.Domain.Users;

public class User : AggregateRoot<Guid>
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    private readonly List<UserShippingAddress> _shippingAddresses = [];
    public IReadOnlyCollection<UserShippingAddress> ShippingAddresses => _shippingAddresses.AsReadOnly();
    private readonly List<UserPasswordHistory> _passwordHistories = [];
    public IReadOnlyCollection<UserPasswordHistory> PasswordHistories => _passwordHistories.ToArray();
    public ImagePath AvatarImagePath { get; private set; } = ImagePath.Default();
    public UserRole Role { get; private set; } = UserRole.Customer;
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public DateTime? LockedAt { get; private set; }
    public bool IsDeleted => DeletedAt is not null;
    public bool IsLocked => LockedAt is not null;

    protected User()
    {
    }

    private User(
        Guid id,
        FullName fullName,
        Email email,
        PasswordHash passwordHash,
        PhoneNumber phoneNumber,
        ImagePath avatarImagePath,
        UserRole role,
        DateTime createdAt) : base(id)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        AvatarImagePath = avatarImagePath;
        Role = role;
        CreatedAt = createdAt;
    }

    public static User Create(
        FullName fullName,
        Email email,
        PasswordHash passwordHash,
        PhoneNumber phoneNumber,
        ImagePath avatarImagePath,
        UserRole role,
        DateTime createdAt)
    {
        return new User(
            id: Guid.NewGuid(),
            fullName: fullName,
            email: email,
            passwordHash: passwordHash,
            phoneNumber: phoneNumber,
            avatarImagePath: avatarImagePath,
            role: role,
            createdAt: createdAt);
    }

    public static User CreateFromPending(PendingRegistration pendingRegistration, DateTime createdAt)
    {
        return User.Create(
            fullName: pendingRegistration.FullName,
            email: pendingRegistration.Id,
            passwordHash: pendingRegistration.PasswordHash,
            phoneNumber: pendingRegistration.PhoneNumber,
            avatarImagePath: ImagePath.Default(),
            role: pendingRegistration.Role,
            createdAt: createdAt);
    }

    public void ChangeFullName(FullName newFullName)
    {
        FullName = newFullName;
    }

    public DomainResult<User> ChangePasswordHash(PasswordHash newPasswordHash)
    {
        if (PasswordHash == newPasswordHash)
            return DomainResult<User>.Failure(new NewPasswordSameAsCurrentPasswordError());

        if (_passwordHistories.Any(p => newPasswordHash == p.PasswordHash))
            return DomainResult<User>.Failure(new NewPasswordRecentlyUsedError());

        var updatedAt = DateTime.UtcNow;
        RemoveExcessPasswordHistories(updatedAt);
        AddCurrentPasswordToHistory(newPasswordHash, updatedAt);
        PasswordHash = newPasswordHash;

        return DomainResult<User>.Success();
    }

    private void AddCurrentPasswordToHistory(PasswordHash passwordHash, DateTime changedAt)
    {
        _passwordHistories.Add(UserPasswordHistory.Create(passwordHash, changedAt));
        UpdatedAt = changedAt;
    }
    private void RemoveExcessPasswordHistories(DateTime changedAt)
    {
        if (_passwordHistories.Count != UserPasswordHistory.MaxStoredPasswordCount) return;
        _passwordHistories.RemoveAt(0);
        UpdatedAt = changedAt;
    }

    public void ChangePhoneNumber(PhoneNumber newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber;
    }

    public void AddShippingAddress(
        FullName recipientName,
        PhoneNumber phoneNumber,
        Address address,
        DateTime updatedAt)
    {
        var userShippingAddress = UserShippingAddress.Create(
            recipientName: recipientName,
            phoneNumber: phoneNumber,
            address: address);
        
        _shippingAddresses.Add(userShippingAddress);
        UpdatedAt = updatedAt;
    }

    public DomainResult<User> ChangeShippingAddress(UserShippingAddress userShippingAddress, DateTime updatedAt)
    {
        var shippingAddress = _shippingAddresses.SingleOrDefault(a => a.Id == userShippingAddress.Id);

        if (shippingAddress is null)
            return DomainResult<User>.Failure(new AddressNotFoundError());

        shippingAddress.Change(
            recipientName: userShippingAddress.RecipientName,
            phoneNumber: userShippingAddress.PhoneNumber,
            address: userShippingAddress.Address);
        UpdatedAt = updatedAt;

        return DomainResult<User>.Success();
    }

    public DomainResult<User> RemoveShippingAddress(UserShippingAddress userShippingAddress, DateTime updatedAt)
    {
        var shippingAddress = _shippingAddresses.SingleOrDefault(a => a.Id == userShippingAddress.Id);

        if (shippingAddress is null)
            return DomainResult<User>.Failure(new AddressNotFoundError());

        _shippingAddresses.Remove(userShippingAddress);
        UpdatedAt = updatedAt;

        return DomainResult<User>.Success();
    }

    public void ChangeAvatarImagePath(ImagePath imagePath, DateTime updatedAt)
    {
        AvatarImagePath = imagePath;
        UpdatedAt = updatedAt;
    }

    public void Lock(DateTime lockedAt)
    {
        if (IsLocked) return;

        LockedAt = lockedAt;
        UpdatedAt = lockedAt;
    }

    public void Unlock(DateTime unlockedAt)
    {
        if (!IsLocked) return;

        LockedAt = null;
        UpdatedAt = unlockedAt;
    }

    public void Delete(DateTime deletedAt)
    {
        if (IsDeleted) return;

        DeletedAt = deletedAt;
        UpdatedAt = deletedAt;
    }
}