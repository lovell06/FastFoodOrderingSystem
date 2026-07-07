using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyOtpController : ControllerBase
    {
        private readonly  IHandler<VerifyOtpCommand, Result<VerifyOtpResponse>> _handler;

        public VerifyOtpController(IHandler<VerifyOtpCommand, Result<VerifyOtpResponse>> handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOtp(VerifyOtpRequest request)
        {
            var command = request.ToCommand();
            var result = await _handler.HandleAsync(command, default);

            if (result.IsSuccess)
                return Created();

            if (result.Error?.Type == ErrorType.Validtion ||
                result.Error?.Type == ErrorType.Business)
                return BadRequest(result.Error);
            return StatusCode(500, result.Error);
        }
    }
}
