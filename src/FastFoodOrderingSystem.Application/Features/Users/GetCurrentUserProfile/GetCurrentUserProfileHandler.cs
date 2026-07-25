using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Users.GetCurrentUserProfile;

public sealed class GetCurrentUserProfileHandler(
    ILogger<GetCurrentUserProfileHandler> logger,
    IUserRepository userRepository,
    IDateTimeProvider clock) : IQueryHandler<GetCurrentUserProfileQuery, PrivateUserProfileResponse>
{
    public async Task<Result<PrivateUserProfileResponse>> HandleAsync(GetCurrentUserProfileQuery query, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;
        var user = await userRepository.GetWithShippingAddressesAsync(query.UserId, cancellationToken);

        if (user is null)
        {
            var err = GetCurrentUserProfileError.UserNotFound;
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<PrivateUserProfileResponse>.Failure(err);
        }
        
        logger.LogInformation($"User with id {query.UserId} has been load. {now}");

        string status = user.IsLocked ? UserStatus.Locked : UserStatus.Active;
        status = user.IsDeleted ? UserStatus.Deleted : status;
        
        logger.LogInformation($"User status: {status}");

        var shippingAddresses = user.ShippingAddresses
            .Select(a => new UserShippingAddressDto(
                a.RecipientName.Value, 
                a.PhoneNumber.Value, 
                a.Address))
            .ToList();
        
        var publicResponse = new PrivateUserProfileResponse(
            FullName: user.FullName.Value,
            AvatarUrl: user.AvatarImagePath.Value,
            Email: user.Email.Value,
            PhoneNumber: user.PhoneNumber.Value,
            Role: user.Role.Code,
            Status: status,
            ShippingAddresses: shippingAddresses);
        
        return Result<PrivateUserProfileResponse>.Success(publicResponse);
    }
}