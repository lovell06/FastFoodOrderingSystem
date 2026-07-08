using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Authentication
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly IHandler<LogoutCommand, Result<LogoutResponse>> _handler;
        public LogoutController(IHandler<LogoutCommand, Result<LogoutResponse>> handler)
        {
            _handler = handler;
        }
        [HttpPost]
        public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand();

            await _handler.HandleAsync(command, cancellationToken);

            return NoContent();
        }
    }
}
