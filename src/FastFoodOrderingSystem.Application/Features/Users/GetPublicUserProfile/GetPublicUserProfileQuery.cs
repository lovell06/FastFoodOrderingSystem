using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Users.GetPublicUserProfile;

public sealed record GetPublicUserProfileQuery (Guid UserId) : IQuery<PublicUserProfileResponse>;