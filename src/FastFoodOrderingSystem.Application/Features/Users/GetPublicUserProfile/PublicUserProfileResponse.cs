namespace FastFoodOrderingSystem.Application.Features.Users.GetPublicUserProfile;

public sealed record PublicUserProfileResponse(
    string FullName,
    string AvatarUrl,
    string Role,
    string Status);