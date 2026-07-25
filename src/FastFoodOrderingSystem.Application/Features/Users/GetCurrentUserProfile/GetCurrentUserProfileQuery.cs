using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Users.GetCurrentUserProfile;

public sealed record GetCurrentUserProfileQuery(Guid UserId) : IQuery<PrivateUserProfileResponse>;