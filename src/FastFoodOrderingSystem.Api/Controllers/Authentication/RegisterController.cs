using System.Net;
using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IHandler<RegisterCommand, Result<RegisterResponse>> _registerHandler;
        public RegisterController(IHandler<RegisterCommand, Result<RegisterResponse>> registerHandler)
        {
            _registerHandler = registerHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = request.ToCommand();
            var result = await _registerHandler.HandleAsync(command, default);

            if (result.IsSuccess)
                return Ok(result.Value);

            var err = new { result.Error?.Code, result.Error?.Message, Type = result.Error?.Type.Value };
            
            if (result.Error?.Type == ErrorType.Conflict)
                return Conflict(err);
            
            if (result.Error?.Type == ErrorType.Validtion)
                return BadRequest(err);
            
            return StatusCode(500, err);
        }
    }
}
