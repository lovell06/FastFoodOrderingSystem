namespace FastFoodOrderingSystem.Application.Features.Customers.Register;

public record VerifyRegisterCommand(string Email, string Code);