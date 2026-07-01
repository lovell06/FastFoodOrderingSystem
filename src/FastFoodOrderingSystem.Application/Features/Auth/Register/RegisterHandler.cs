using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public sealed class RegisterHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitWork _unitWork;
    private readonly IPasswordHashService _passwordHashService;
    public RegisterHandler(IUserRepository userRepository, 
        IUnitWork unitWork,
        IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _unitWork = unitWork;
        _passwordHashService = passwordHashService;
    }
    // public Result<RegisterResponse> Handle(RegisterCommand command)
    // {
    //     var fullName = FullName.Create(command.FullName);
    //     var email = Email.Create(command.Email);
    //     var password = Password.Create(command.Password);
    //     var passwordHash = _passwordHashService.Hash(password);
    //     var phoneNumber = PhoneNumber.Create(command.PhoneNumber);
    // }
}