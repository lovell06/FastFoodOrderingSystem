using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using FastFoodOrderingSystem.Application.Features.Auth.Register;
using FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;
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
        private readonly IHandler<VerifyOtpCommand, Result<Unit>> _verifyOtpHandler;

        public AuthController(
            IHandler<LoginCommand, Result<LoginResponse>> loginHandler, 
            IHandler<LogoutCommand, Result<Unit>> logoutHandler, 
            IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> refreshTokenHandler, 
            IHandler<RegisterCommand, Result<Unit>> registerHandler, 
            IHandler<VerifyOtpCommand, Result<Unit>> verifyOtpHandler)
        {
            _loginHandler = loginHandler;
            _logoutHandler = logoutHandler;
            _refreshTokenHandler = refreshTokenHandler;
            _registerHandler = registerHandler;
            _verifyOtpHandler = verifyOtpHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await _loginHandler.HandleAsync(command, cancellationToken);

            if (result.IsFailure)
            {
                Error err = result.Error!;
                if (err.Type == ErrorType.Validtion || err.Type == ErrorType.Failure)
                    return BadRequest(new { err.Code, err.Message, err.Type.Value });

                return StatusCode(500, new {err.Code, err.Message, err.Type.Value});
            }

            return Ok(result.Value);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand();

            await _logoutHandler.HandleAsync(command, cancellationToken);

            return NoContent();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await _refreshTokenHandler.HandleAsync(command, cancellationToken);

            if (result.IsFailure)
                return Unauthorized(new { result.Error!.Code, result.Error.Message, result.Error.Type.Value });

            return Ok(result.Value);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = request.ToCommand();
            var result = await _registerHandler.HandleAsync(command, default);

            if (result.IsSuccess)
                return Ok();

            var err = new { result.Error?.Code, result.Error?.Message, Type = result.Error?.Type.Value };

            if (result.Error?.Type == ErrorType.Conflict)
                return Conflict(err);

            if (result.Error?.Type == ErrorType.Validtion)
                return BadRequest(err);

            return StatusCode(500, err);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpRequest request)
        {
            var command = request.ToCommand();
            var result = await _verifyOtpHandler.HandleAsync(command, default);

            if (result.IsSuccess)
                return Created();

            if (result.Error?.Type == ErrorType.Validtion ||
                result.Error?.Type == ErrorType.Business)
                return BadRequest(result.Error);
            return StatusCode(500, result.Error);
        }
    }
}
