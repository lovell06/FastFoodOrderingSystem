using FastFoodOrderingSystem.Application.Common.Errors;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Mapping;

public static class ErrorMapper
{
    public static IActionResult ToActionResult(this Error error, ControllerBase controller)
    {
        object response = new
        {
            error.Code,
            error.Message
        };

        if (error.Type == ErrorType.Validtion || error.Type == ErrorType.Business)
            return controller.BadRequest(response);
        if (error.Type == ErrorType.Conflict)
            return controller.Conflict(response);
        if (error.Type == ErrorType.Forbidden)
            return controller.Forbid();
        if (error.Type == ErrorType.Unathorized)
            return controller.Unauthorized();
        if (error.Type == ErrorType.NotFound)
            return controller.NotFound();
        if (error == SystemError.Unexpected)
            return controller.StatusCode(500, SystemError.Unexpected);

        throw new ArgumentOutOfRangeException(nameof(error.Type));
    }
}