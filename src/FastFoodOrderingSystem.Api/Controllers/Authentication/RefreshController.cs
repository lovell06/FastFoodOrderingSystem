using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshController : ControllerBase
    {
        private readonly IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> _handler;

        public RefreshController(IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> handler)
        {
            _handler = handler;
        }
        [HttpPost]
        public async Task<IActionResult> Refresh(RefreshRequest request, CancellationToken cancellationToken)
        {
            var command = request.ToCommand();

            var result = await _handler.HandleAsync(command, cancellationToken);

            if (result.IsFailure)
                return Unauthorized(new { result.Error!.Code, result.Error.Message, result.Error.Type.Value });

            return Ok(result.Value);
        }
    }
}
