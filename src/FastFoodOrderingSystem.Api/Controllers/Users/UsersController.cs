using FastFoodOrderingSystem.Api.Contracts.Users;
using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Abstractions.Mediator;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("profile/{id:guid}")]
    public async Task<IActionResult> Profile(System.Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.SendAsync<GetProfileQuery, UserProfileResponse>(
            request: new GetProfileQuery(id),
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.ToActionResult(this);
    }

    [HttpPatch("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _mediator.SendAsync<UpdateProfileCommand, Unit>(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.ToActionResult(this);
    }
}