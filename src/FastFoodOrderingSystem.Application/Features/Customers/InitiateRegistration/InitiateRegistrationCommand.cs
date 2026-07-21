namespace FastFoodOrderingSystem.Application.Features.Customers.InitiateRegistration;

public sealed record InitiateRegistrationCommand(string FullName,
    string Email,
    string Password,
    string PhoneNumber);