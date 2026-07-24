using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Users.GetProfile;

public sealed record Guid (System.Guid UserId) : IQuery<UserProfileResponse>;