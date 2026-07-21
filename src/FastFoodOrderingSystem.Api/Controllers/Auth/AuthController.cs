using System.Security.Claims;
using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;
using FastFoodOrderingSystem.Application.Features.Auth.CompleteForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using FastFoodOrderingSystem.Application.Features.Customers.CompleteRegistration;
using FastFoodOrderingSystem.Application.Features.Customers.InitiateRegistration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IHandler<LoginCommand, Result<LoginResponse>> _loginHandler;
    private readonly IHandler<LogoutCommand, Result<Unit>> _logoutHandler;
    private readonly IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> _refreshTokenHandler;
    private readonly IHandler<InitiateRegistrationCommand, Result<Unit>> _InitiateRegistrationHandler;
    private readonly IHandler<CompleteRegistrationCommand, Result<Unit>> _completeRegistrationHandler;
    private readonly IHandler<InitiateForgotPasswordCommand, Result<Unit>> _initiateForgotPasswordHandler;
    private readonly IHandler<CompleteForgotPasswordCommand, Result<Unit>> _completeForgotPasswordHandler;
    private readonly IHandler<ChangePasswordCommand, Result<Unit>> _changePasswordHandler;

    public AuthController(
        IHandler<LoginCommand, Result<LoginResponse>> loginHandler,
        IHandler<LogoutCommand, Result<Unit>> logoutHandler,
        IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> refreshTokenHandler,
        IHandler<InitiateRegistrationCommand, Result<Unit>> initiateRegistrationHandler,
        IHandler<CompleteRegistrationCommand, Result<Unit>> completeRegistrationHandler,
        IHandler<InitiateForgotPasswordCommand, Result<Unit>> initiateForgotPasswordHandler, 
        IHandler<CompleteForgotPasswordCommand, Result<Unit>> completeForgotPasswordHandler, 
        IHandler<ChangePasswordCommand, Result<Unit>> changePasswordHandler)
    {
        _loginHandler = loginHandler;
        _logoutHandler = logoutHandler;
        _refreshTokenHandler = refreshTokenHandler;
        _InitiateRegistrationHandler = initiateRegistrationHandler;
        _completeRegistrationHandler = completeRegistrationHandler;
        _initiateForgotPasswordHandler = initiateForgotPasswordHandler;
        _completeForgotPasswordHandler = completeForgotPasswordHandler;
        _changePasswordHandler = changePasswordHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await _loginHandler.HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error!.ToActionResult(this);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await _logoutHandler.HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return result.Error!.ToActionResult(this);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await _refreshTokenHandler.HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error!.ToActionResult(this);
    }

    [HttpPost("registrations/initiate")]
    public async Task<IActionResult> InitiateRegistration(
        InitiateRegistrationRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _InitiateRegistrationHandler
            .HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error!.ToActionResult(this);
    }

    [HttpPost("registrations/complete")]
    public async Task<IActionResult> CompleteRegistration(
        CompleteRegistrationRequest request, 
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _completeRegistrationHandler
            .HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return Created();

        return result.Error!.ToActionResult(this);
    }

    [HttpPost("forgot-password/initiate")]
    public async Task<IActionResult> InitiateForgotPassword(
        InitiateForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await _initiateForgotPasswordHandler.HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error!.ToActionResult(this);
    }

    [HttpPost("forgot-password/complete")]
    public async Task<IActionResult> CompleteForgotPassword(
        CompleteForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await _completeForgotPasswordHandler.HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error!.ToActionResult(this);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
        var command = request.ToCommand(userId);

        var result = await _changePasswordHandler.HandleAsync(command, cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error!.ToActionResult(this);
    }
}