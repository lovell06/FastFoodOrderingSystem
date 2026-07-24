using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Users.GetProfile;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;

public sealed class UpdateProfileHandler(
    IUserRepository userRepository,
    ICachePolicy<UpdateProfileCommand> cachePolicy,
    ICacheStore<UserProfileResponse> cacheStore,
    ILogger<UpdateProfileHandler> logger,
    IDateTimeProvider clock) : ICommandHandler<UpdateProfileCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        var now = clock.UtcNow;
        
        var userId = command.UserId;
        
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            logger.LogError($"User not found. User id: {userId}. {now}");
            return Result<Unit>.Failure(UpdateProfileError.Unauthorized);
        }

        if (command.FullName is not null)
        {
            var result = FullName.Create(command.FullName);

            if (result.IsFailure)
            {
                var err = Error.Validation(result.Error.Code, result.Error.Message);
                logger.LogError($"{err.Type.Value}. {err.Code}. {err.Message}. {now}");

                return Result<Unit>.Failure(err);
            }

            user.ChangeFullName(result.Value);
        }

        if (command.PhoneNumber is not null)
        {
            var result = PhoneNumber.Create(command.PhoneNumber);
            
            if (result.IsFailure)
            {
                var err = Error.Validation(result.Error.Code, result.Error.Message);
                logger.LogError($"{err.Type.Value}. {err.Code}. {err.Message}. {now}");

                return Result<Unit>.Failure(err);
            }

            user.ChangePhoneNumber(result.Value);
        }

        await cacheStore.RemoveAsync(cachePolicy.GetKey(command), cancellationToken);

        return Result<Unit>.Success(Unit.Value);
    }
}