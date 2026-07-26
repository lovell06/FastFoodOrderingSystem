using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Application.Features.Users.GetPrivateUserProfile;

public sealed record PrivateUserProfileResponse(
    string FullName,
    string AvatarUrl,
    string Email,
    string PhoneNumber,
    string Role,
    string Status,
    List<UserShippingAddressDto> ShippingAddresses);
    
public sealed record UserShippingAddressDto(
    string RecipientName,
    string PhoneNumber,
    Address Address);