using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Features.Users.GetPrivateUserProfile;
using FastFoodOrderingSystem.Application.Features.Users.GetPublicUserProfile;
using FastFoodOrderingSystem.Application.Features.Users.UpdateProfile;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Application.Features.Users;

internal static class DependencyInjection
{
    public static IServiceCollection AddUserHandlers(this IServiceCollection services)
    {
        services.AddQueryHandler<GetPrivateUserProfileQuery, PrivateUserProfileResponse, GetPrivateUserProfileHandler>();
        services.AddQueryHandler<GetPublicUserProfileQuery, PublicUserProfileResponse, GetPublicUserProfileHandler>();
        services.AddCommandHandler<UpdateProfileCommand, Unit, UpdateProfileHandler>();
        
        return services;
    }
}