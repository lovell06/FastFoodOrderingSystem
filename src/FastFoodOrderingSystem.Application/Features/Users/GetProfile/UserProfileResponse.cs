using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Features.Users.GetProfile;

public sealed record UserProfileResponse(
    string FullName,
    string Email,
    string PhoneNumber,
    string Role,
    string Status,
    List<UserShippingAddress> Addresses);