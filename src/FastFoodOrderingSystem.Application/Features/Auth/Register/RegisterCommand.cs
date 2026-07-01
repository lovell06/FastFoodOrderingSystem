namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public sealed record RegisterCommand(string FullName,
    string Email,
    string Password,
    string PhoneNumber);