using FastFoodOrderingSystem.Application.Common.Cqrs;

namespace FastFoodOrderingSystem.Application.Features.Users.GetProfile;

public sealed record GetProfileQuery (Guid UserId) : IQuery<GetProfileResponse>;