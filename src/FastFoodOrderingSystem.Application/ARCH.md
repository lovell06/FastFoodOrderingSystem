# Application layer

## Responsibility

- Implements use cases.
- Coordinates the Domain

## Contains

- Service Abstractions.
- Query / Command handler.
- Configurations.
- DTOs.
- Decorators.

## Must NOT

- Access EF Core directly.
- Know DBMSs.
- Know Cache Services.
- Know ASP.NET Core Framework.

## Dependency

- Domain layer.
- its abstraction.

## Flow

```aiignore
Command

↓

Decorator

↓

Handler

↓

Repository

↓

UnitOfWork
```

## Folder structure

```
FastFoodOrderingSystem.Application
│
├── Abstractions/
│   │
│   ├── Authentication/
│   │   ├── IAccessTokenProvider.cs
│   │   ├── IOtpHashService.cs
│   │   ├── IOtpService.cs
│   │   ├── IPasswordGenerator.cs
│   │   ├── IPasswordHashService.cs
│   │   ├── IRefreshTokenGenerator.cs
│   │   └── ...
│   │  
│   ├── Cache/
│   │   ├── ForgotPasswordOtp/
│   │   │   ├── ForgotPasswordOtp.cs
│   │   │   └── IForgotPasswordOtpStore.cs
│   │   ├── PendingRegistration/
│   │   │   ├── IPendingRegistrationStore.cs
│   │   │   └── PendingRegistration.cs
│   │   │── RefreshToken/
│   │   │   ├── IRefreshTokenStore.cs
│   │   │   └── RefreshToken.cs
│   │   └── ...
│   │  
│   ├── Configurations/
│   │   ├── IAccessTokenConfiguration.cs
│   │   ├── IEmailConfiguration.cs
│   │   ├── IOtpConfiguration.cs
│   │   ├── IRefreshTokenConfiguration.cs
│   │   └── ...
│   │  
│   ├── Emails/
│   │   ├── EmailContent.cs
│   │   ├── IEmailSender.cs
│   │   └── ...
│   │   
│   ├── Mediator/
│   │   └── IMediator.cs
│   │   
│   ├── Persistence/
│   │   ├── IUnitWork.cs
│   │   ├── IUserRepository.cs
│   │   └── ...
│   │   
│   ├── storage/
│   │   ├── FileStorageCategory.cs
│   │   ├── FileStorageOptions.cs
│   │   ├── IFileStorage.cs
│   │   └── ...
│   │   
│   └── Time/
│   │   ├── IDateTimeProvider.cs
│   │   └── ...
│   └── ...
│  
├── Common/
│   │
│   ├── Cqrs/
│   │   ├── Decorators/
│   │   │   ├── Commands/
│   │   │   │   ├── CommandHandlerDecorator.cs
│   │   │   │   └── ...
│   │   │   ├── Handlers/
│   │   │   │   ├── HandlerDecorator.cs
│   │   │   │   └── ...
│   │   │   └── Queries/
│   │   │       ├── QueryHandlerDecorator.cs
│   │   │       └── ...
│   │   │
│   │   ├── IRequest.cs
│   │   ├── ICommand.cs
│   │   ├── IQuery.cs
│   │   ├── IHandler.cs
│   │   ├── ICommandHandler.cs
│   │   ├── IQueryHandler.cs
│   │   └── Unit.cs
│   │  
│   ├── Errors/
│   │   └── SystemError.cs
│   │  
│   └── Results/
│       ├── Error.cs
│       ├── ErrorType.cs
│       └── Result.cs
│   
└── Features/
│   │
│   ├── Auth/
│   │   ├── ChangePassword/
│   │   │   ├── ChangePasswordCommand.cs
│   │   │   ├── ChangePasswordError.cs
│   │   │   └── ChangePasswordHandler.cs
│   │   │  
│   │   ├── CompleteForgotPassword/
│   │   │   ├── CompleteForgotPasswordCommand.cs
│   │   │   └── CompleteForgotPasswordHandler.cs
│   │   │  
│   │   ├── InitiateForgotPassword/
│   │   │   ├── InitiateForgotPasswordCommand.cs
│   │   │   ├── InitiateForgotPasswordError.cs
│   │   │   └── InitiateForgotPasswordHandler.cs
│   │   │  
│   │   ├── Login/
│   │   │   ├── Dtos/
│   │   │   │   ├── RefreshTokenDto.cs
│   │   │   │   └── UserDto.cs
│   │   │   ├── LoginCommand.cs
│   │   │   ├── LoginError.cs
│   │   │   ├── LoginHandler.cs
│   │   │   └── LoginResponse.cs
│   │   │  
│   │   ├── Logout/
│   │   │   ├── LogoutCommand.cs
│   │   │   └── LogoutHandler.cs
│   │   │  
│   │   ├── Refresh/
│   │   │   ├── Dtos/
│   │   │   │   ├── AccessTokenDto.cs
│   │   │   │   └── RefreshTokenDto.cs
│   │   │   ├── RefreshTokenCommand.cs
│   │   │   ├── RefreshTokenError.cs
│   │   │   ├── RefreshTokenHandler.cs
│   │   │   └── RefreshTokenResponse.cs
│   │   │
│   │   └── DependencyInjection.cs                  # Add Authentication features
│   │  
│   ├── Customers/
│   │   ├── CompleteRegistration/
│   │   │   ├── CompleteRegistrationCommand.cs
│   │   │   ├── CompleteRegistrationError.cs
│   │   │   └── CompleteRegistrationHandler.cs
│   │   │  
│   │   ├── InitiateRegistration/
│   │   │   ├── InitiateRegistrationCommand.cs
│   │   │   ├── InitiateRegistrationError.cs
│   │   │   └── InitiateRegistrationHandler.cs
│   │   │
│   │   └── DependencyInjection.cs                  # Add customer features
│   │   
│   ├── Users/
│   │   ├── GetProfile/   
│   │   │   ├── CompleteRegistrationCommand.cs
│   │   │   ├── CompleteRegistrationError.cs
│   │   │   └── CompleteRegistrationHandler.cs
│   │   ├── ...
│   │   │
│   │   └── DependencyInjection.cs                  # Add user features
│   │
│   └── ...  
│
└── DependencyInjection.cs      # Add Application Layer
```

# Patterns

- CQRS
- Decorator
- Mediator