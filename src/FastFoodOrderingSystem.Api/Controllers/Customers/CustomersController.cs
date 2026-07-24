using FastFoodOrderingSystem.Api.Contracts.Authentication;
using FastFoodOrderingSystem.Api.Mapping;
using FastFoodOrderingSystem.Application.Abstractions.Mediator;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Features.Customers.CompleteRegistration;
using FastFoodOrderingSystem.Application.Features.Customers.InitiateRegistration;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodOrderingSystem.Api.Controllers.Customers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    [HttpPost("registrations/initiate")]
    public async Task<IActionResult> InitiateRegistration(
        InitiateRegistrationRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _mediator.SendAsync<InitiateRegistrationCommand, Unit>(
            request: command, 
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Ok();

        return result.Error.ToActionResult(this);
    }

    [HttpPost("registrations/complete")]
    public async Task<IActionResult> CompleteRegistration(
        CompleteRegistrationRequest request, 
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _mediator.SendAsync<CompleteRegistrationCommand, Unit>(
            request: command, 
            cancellationToken: cancellationToken);

        if (result.IsSuccess)
            return Created();

        return result.Error.ToActionResult(this);
    }
}