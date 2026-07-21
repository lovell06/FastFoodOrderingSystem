using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Customers.CompleteRegistration;

public record CompleteRegistrationCommand(
    string Email, 
    string Code) : ICommand<Unit>;