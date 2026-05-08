using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Domain.Users;

public class UserShippingAddress : Entity<long>
{
    public FullName RecipientName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Address Address { get; private set; }

    private UserShippingAddress(FullName recipientName, PhoneNumber phoneNumber, Address address)
    {
        RecipientName = recipientName;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public static UserShippingAddress Create(FullName recipientName, PhoneNumber phoneNumber, Address address)
    {
        return new UserShippingAddress(recipientName, phoneNumber, address);
    }

    public void Change(FullName recipientName, PhoneNumber phoneNumber, Address address)
    {
        RecipientName = recipientName;
        PhoneNumber = phoneNumber;
        Address = address;
    }
}