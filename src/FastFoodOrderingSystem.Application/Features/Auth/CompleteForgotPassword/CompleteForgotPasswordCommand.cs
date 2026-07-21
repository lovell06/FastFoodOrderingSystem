namespace FastFoodOrderingSystem.Application.Features.Auth.CompleteForgotPassword;

public sealed record CompleteForgotPasswordCommand(string Email, string OtpCode);