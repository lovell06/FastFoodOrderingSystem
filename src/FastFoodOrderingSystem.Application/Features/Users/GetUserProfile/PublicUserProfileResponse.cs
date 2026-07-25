namespace FastFoodOrderingSystem.Application.Features.Users.GetUserProfile;

public sealed record PublicUserProfileResponse(
    string FullName,
    string AvatarUrl,
    string Role,
    string Status);