using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Users.GetPrivateUserProfile;

public sealed record GetPrivateUserProfileQuery(Guid UserId) : IQuery<PrivateUserProfileResponse>;