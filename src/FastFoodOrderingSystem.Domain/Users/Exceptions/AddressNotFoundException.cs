using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Users.Exceptions;

public class AddressNotFoundException : DomainException
{
    public AddressNotFoundException() : base(
        code: "address.not_found",
        message: "Address is not found.")
    {
    }
}