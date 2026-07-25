using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Users.GetUserProfile;

public class GetUserProfileHandler(
    IUserRepository userRepository,
    ILogger<GetUserProfileHandler> logger,
    IDateTimeProvider clock)
    : IQueryHandler<GetUserProfileQuery, PublicUserProfileResponse>
{
    public async Task<Result<PublicUserProfileResponse>> HandleAsync(GetUserProfileQuery query, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;
        var user = await userRepository.GetWithShippingAddressesAsync(query.UserId, cancellationToken);

        if (user is null)
        {
            var err = GetUserProfileError.UserNotFound;
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