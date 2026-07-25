using FastFoodOrderingSystem.Api.Contracts.Users;
using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Mediator;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Features.Users.GetCurrentUserProfile;
using FastFoodOrderingSystem.Application.Features.Users.GetUserProfile;
using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace FastFoodOrderingSystem.Api.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    IMediator mediator,
    ICurrentUser currentUser) : ControllerBase
{
    [HttpGet("profile/{id:guid}")]
    public async Task<IActionResult> Profile(System.Guid id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.SendAsync<GetUserProfileQuery, PublicUserProfileResponse>(
            request: new GetUserProfileQuery(id),
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.ToActionResult(this);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> CurrentUserProfile(CancellationToken cancellationToken)
    {
        var result =
            await mediator.SendAsync<GetCurrentUserProfileQuery, PrivateUserProfileResponse>(
                request: new GetCurrentUserProfileQuery(currentUser.Id), 
                cancellationToken: cancellationToken);
    
        if (result.IsSuccess)
            return Ok(result.Value);
    
        return result.Error.ToActionResult(this);
    }

    [HttpPatch("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await mediator.SendAsync<UpdateProfileCommand, Unit>(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.ToActionResult(this);
    }
}