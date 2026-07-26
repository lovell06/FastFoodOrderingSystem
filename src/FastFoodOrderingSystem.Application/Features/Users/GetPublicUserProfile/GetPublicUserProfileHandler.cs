using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Users.GetPublicUserProfile;

public class GetPublicUserProfileHandler(
    IUserRepository userRepository,
    ILogger<GetPublicUserProfileHandler> logger,
    IDateTimeProvider clock)
    : IQueryHandler<GetPublicUserProfileQuery, PublicUserProfileResponse>
{
    public async Task<Result<PublicUserProfileResponse>> HandleAsync(GetPublicUserProfileQuery query, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;
        var user = await userRepository.GetWithShippingAddressesAsync(query.UserId, cancellationToken);

        if (user is null)
        {
            var err = GetPublicUserProfileError.UserNotFound;
            logger.LogError($"{err.Type}. {err.Code}. {err.Message}. {now}");
            return Result<PublicUserProfileResponse>.Failure(err);
        }
        
        logger.LogInformation($"User with id {query.UserId} has been load. {now}");

        string status = user.IsLocked ? UserStatus.Locked : UserStatus.Active;
        status = user.IsDeleted ? UserStatus.Deleted : status;
        
        logger.LogInformation($"User status: {status}");
        
        var publicResponse = new PublicUserProfileResponse(
            FullName: user.FullName.Value,
            AvatarUrl: user.AvatarImagePath.Value,
            Role: user.Role.Code,
            Status: status);
        
        return Result<PublicUserProfileResponse>.Success(publicResponse);
    }
}