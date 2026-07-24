using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Abstractions.Mediator;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
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
    public async Task<IActionResult> Profile(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.SendAsync<GetProfileQuery, UserProfileResponse>(
            request: new GetProfileQuery(id),
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error!.ToActionResult(this);
    }
}