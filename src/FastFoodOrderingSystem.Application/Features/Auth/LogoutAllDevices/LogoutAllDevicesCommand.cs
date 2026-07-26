using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Auth.LogoutAllDevices;

public record LogoutAllDevicesCommand(Guid UserId) : ICommand<Unit>;