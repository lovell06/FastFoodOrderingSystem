using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Application.Common.Errors;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHandler<LoginCommand, Result<LoginResponse>> _handler;
        public LoginController(IHandler<LoginCommand, Result<LoginResponse>> handler)
        {
            _handler = handler;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await _handler.HandleAsync(command, cancellationToken);

            if (result.IsFailure)
            {
                Error err = result.Error!;
                if (err.Type == ErrorType.Validtion || err.Type == ErrorType.Failure)
                    return BadRequest(new { err.Code, err.Message, err.Type.Value });

                return StatusCode(500, new {err.Code, err.Message, err.Type.Value});
            }

            return Accepted(result.Value);
        }
    }
}
