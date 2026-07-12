namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public record VerifyRegisterCommand(string Email, string Code);