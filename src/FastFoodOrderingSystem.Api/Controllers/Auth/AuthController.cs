using System.Security.Claims;
using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Mediator;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;
using FastFoodOrderingSystem.Application.Features.Auth.CompleteForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.LogoutAllDevices;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IMediator mediator,
    ICurrentUser currentUser) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await mediator.SendAsync<LoginCommand, LoginResponse>(
            request: command, 
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.ToActionResult(this);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken = default)
    {
        var userId = currentUser.Id;
        
        var command = request.ToCommand(userId);

        var result = await mediator.SendAsync<LogoutCommand, Unit>(
            request: command, 
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return result.Error.ToActionResult(this);
    }

    [Authorize]
    [HttpPost("logout/all-devices")]
    public async Task<IActionResult> LogoutAllDevice(CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;
        var command = new LogoutAllDevicesCommand(userId);
        var result = await mediator.SendAsync<LogoutAllDevicesCommand, Unit>(
            request: command,
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.ToActionResult(this);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await mediator
            .SendAsync<RefreshTokenCommand, RefreshTokenResponse>(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.ToActionResult(this);
    }

    [HttpPost("forgot-password/initiate")]
    public async Task<IActionResult> InitiateForgotPassword(
        InitiateForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await mediator.SendAsync<InitiateForgotPasswordCommand, Unit>(
            request: command, 
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error.ToActionResult(this);
    }

    [HttpPost("forgot-password/complete")]
    public async Task<IActionResult> CompleteForgotPassword(
        CompleteForgotPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();

        var result = await mediator.SendAsync<CompleteForgotPasswordCommand, Unit>(
            request: command, 
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error.ToActionResult(this);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;
        var command = request.ToCommand(userId);

        var result = await mediator.SendAsync<ChangePasswordCommand, Unit>(
            request: command, 
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error.ToActionResult(this);
    }
}