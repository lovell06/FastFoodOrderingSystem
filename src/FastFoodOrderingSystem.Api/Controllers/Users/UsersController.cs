using FastFoodOrderingSystem.Api.Contracts.Users;
using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Mediator;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Features.Users.GetPrivateUserProfile;
using FastFoodOrderingSystem.Application.Features.Users.GetPublicUserProfile;
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
        var result = await mediator.SendAsync<GetPublicUserProfileQuery, PublicUserProfileResponse>(
            request: new GetPublicUserProfileQuery(id),
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
            await mediator.SendAsync<GetPrivateUserProfileQuery, PrivateUserProfileResponse>(
                request: new GetPrivateUserProfileQuery(currentUser.Id), 
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