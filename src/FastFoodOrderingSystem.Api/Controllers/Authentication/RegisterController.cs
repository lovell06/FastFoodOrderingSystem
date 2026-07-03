using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterHandler _registerHandler;
        public RegisterController(RegisterHandler registerHandler)
        {
            _registerHandler = registerHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = request.ToCommand();
            var result = await _registerHandler.HandleAsync(command);

            if (result.IsSuccess)
                return Ok(result.Value);

            var err = new { result.Error?.Code, result.Error?.Message, Type = result.Error?.Type.Value };
            if (result.Error?.Type == ErrorType.Conflict)
                return Conflict(err);
            return BadRequest(err);
        }
    }
}
