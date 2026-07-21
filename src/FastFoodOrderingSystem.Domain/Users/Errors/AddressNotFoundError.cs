using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.Errors;

public sealed record AddressNotFoundError : DomainError
{
    public AddressNotFoundError() : base(
        Code: "address.not_found", 
        Message: "Address is not found.")
    {
    }
}