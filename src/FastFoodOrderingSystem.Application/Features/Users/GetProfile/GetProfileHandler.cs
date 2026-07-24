using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Users.GetProfile;

public class GetProfileHandler : IQueryHandler<GetProfileQuery, UserProfileResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<GetProfileHandler> _logger;
    private readonly IDateTimeProvider _clock;

    public GetProfileHandler(IUserRepository userRepository, ILogger<GetProfileHandler> logger, IDateTimeProvider clock)
    {
        _userRepository = userRepository;
        _logger = logger;
        _clock = clock;
    }
    
    public async Task<Result<UserProfileResponse>> HandleAsync(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var now = _clock.UtcNow;
        var user = await _userRepository.GetWithShippingAddressesAsync(query.UserId, cancellationToken);

        if (user is null)
        {
            var err = GetProfileError.UserNotFound;
            _logger.LogError($"{err.Code}. {err.Message}. {now}");
            return Result<UserProfileResponse>.Failure(err);
        }
        
        _logger.LogInformation($"User with id {query.UserId} has been load. {now}");

        string status = user.IsLocked ? UserStatus.Locked : UserStatus.Active;
        status = user.IsDeleted ? UserStatus.Deleted : status;
        
        _logger.LogInformation($"User status: {status}");

        var response = new UserProfileResponse(
            FullName: user.FullName.Value,
            Email: user.Email.Value,
            PhoneNumber: user.PhoneNumber.Value,
            Role: user.Role.Code,
            Status: status,
            Addresses: user.ShippingAddresses.ToList());
        
        return Result<UserProfileResponse>.Success(response);
    }
}