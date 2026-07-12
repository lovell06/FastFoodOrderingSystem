using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.ForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using FastFoodOrderingSystem.Application.Features.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHandler<LoginCommand, Result<LoginResponse>> _loginHandler;
        private readonly IHandler<LogoutCommand, Result<Unit>> _logoutHandler;
        private readonly IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> _refreshTokenHandler;
        private readonly IHandler<RegisterCommand, Result<Unit>> _registerHandler;
        private readonly IHandler<VerifyRegisterCommand, Result<Unit>> _verifyOtpHandler;
        private readonly IHandler<ForgotPasswordCommand, Result<Unit>> _forgotPasswordHandler;
        private readonly IHandler<VerifyForgotPasswordCommand, Result<Unit>> _verifyForgotPasswordHandler;

        public AuthController(
            IHandler<LoginCommand, Result<LoginResponse>> loginHandler,
            IHandler<LogoutCommand, Result<Unit>> logoutHandler,
            IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> refreshTokenHandler,
            IHandler<RegisterCommand, Result<Unit>> registerHandler,
            IHandler<VerifyRegisterCommand, Result<Unit>> verifyOtpHandler,
            IHandler<ForgotPasswordCommand, Result<Unit>> forgotPasswordHandler, 
            IHandler<VerifyForgotPasswordCommand, Result<Unit>> verifyForgotPasswordHandler)
        {
            _loginHandler = loginHandler;
            _logoutHandler = logoutHandler;
            _refreshTokenHandler = refreshTokenHandler;
            _registerHandler = registerHandler;
            _verifyOtpHandler = verifyOtpHandler;
            _forgotPasswordHandler = forgotPasswordHandler;
            _verifyForgotPasswordHandler = verifyForgotPasswordHandler;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = request.ToCommand();
            var result = await _registerHandler.HandleAsync(command, default);

            if (result.IsSuccess)
                return Ok();

            return result.Error!.ToActionResult(this);
        }

        [HttpPost("register/verify")]
        public async Task<IActionResult> VerifyRegister(VerifyRegisterRequest request)
        {
            var command = request.ToCommand();
            var result = await _verifyOtpHandler.HandleAsync(command, default);

            if (result.IsSuccess)
                return Created();

            return result.Error!.ToActionResult(this);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await _forgotPasswordHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
                return Ok();

            return result.Error!.ToActionResult(this);
        }

        [HttpPost("forgot-password/verify")]
        public async Task<IActionResult> VerifyForgotPassword(
            VerifyForgotPasswordRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await _verifyForgotPasswordHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
                return Ok();

            return result.Error!.ToActionResult(this);
        }
    }
}
