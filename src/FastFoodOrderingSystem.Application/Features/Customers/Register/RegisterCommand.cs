namespace FastFoodOrderingSystem.Application.Features.Customers.Register;

public sealed record RegisterCommand(string FullName,
    string Email,
    string Password,
    string PhoneNumber);