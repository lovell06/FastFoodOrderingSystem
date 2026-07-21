using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Users.GetProfile;

public class GetProfileHandler : IQueryHandler<GetProfileQuery, GetProfileResponse>
{
    public Task<Result<GetProfileResponse>> HandleAsync(GetProfileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}