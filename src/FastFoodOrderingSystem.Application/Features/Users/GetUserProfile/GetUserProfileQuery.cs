using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Users.GetUserProfile;

public sealed record GetUserProfileQuery (Guid UserId) : IQuery<PublicUserProfileResponse>;