namespace FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;

public record VerifyOtpCommand(string Email, string Code);