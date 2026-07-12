namespace FastFoodOrderingSystem.Application.Features.Auth.ForgotPassword;

public sealed record VerifyForgotPasswordCommand(string Email, string OtpCode);