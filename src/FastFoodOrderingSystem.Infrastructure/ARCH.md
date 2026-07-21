# Infrastructure layer

## Responsibility

Implement application's abstractions.

## Contains

- Entity Framework Core.
- PosgreSQL.
- Redis.
- JWT.
- File storage.
- Outbox.
- Worker.
- Email sender.
- Integration events.
- Date time provider.
- Option from environment variable.


## Should NEVER contain

Business rules.

## Dependency

- Domain.
- Application's abstractions.

## Folder structure
```
FastFoodOrderingSystem.Infrastructure/
├── Authentication/
│   ├── DependencyInjection.cs
│   ├── JwtProvider.cs
│   ├── OtpHashService.cs
│   ├── OtpService.cs
│   ├── PasswordHashService.cs
│   ├── PasswordRandomStringGenerator.cs
│   ├── RefreshTokenGenerator.cs
│   └── ...
│   
├── Cache/
│   │
│   ├── DependencyInjection.cs
│   │
│   ├── ...
│   │
│   └── Redis/
│       ├── ForgotPasswordOtp/
│       │   ├── RedisForgotPasswordOtpCache.cs
│       │   └── ...
│       │  
│       ├── Mappers/
│       │   ├── ForgotPasswordOtpMapper.cs
│       │   ├── PendingRegistrationMapper.cs
│       │   ├── RefreshTokenMapper.cs
│       │   └── ...
│       │  
│       ├── PendingRegistration/
│       │   ├── RedisPendingRegistrationCache.cs
│       │   └── ...
│       │  
│       ├── RefreshToken/
│       │   ├── RedisRefreshTokenCache.cs
│       │   └── ...
│       │  
│       ├── Snapshots/
│       │   ├── ForgotPasswordOtpSnapshot.cs
│       │   ├── PendingRegistrationSnapshot.cs
│       │   ├── RefreshTokenSnapshot.cs
│       │   ├── RedisRefreshTokenCache.cs
│       │   └── ...
│       │  
│       ├── ...
│       │  
│       └── RedisKeyProvider.cs
│  
├── Configurations
│   ├── DependencyInjection.cs
│   ├── ...
│   ├── AccessTokenConfiguration.cs
│   ├── GmailConfiguration.cs
│   ├── OtpConfiguration.cs
│   └── RefreshTokenConfiguration.cs
│
├── Emails
│   ├── DependencyInjection.cs
│   ├── ...
│   └── GmailSender.cs
│   
├── Eventing
│   ├── Abstractions
│   │   ├── IEvent.cs
│   │   ├── IEventDispatcher.cs
│   │   └── IEventHandler.cs
│   ├── EventMappers
│   │   └── DomainEventMappers.cs
│   ├── IntegrationEventDispatchers
│   │   ├── DependencyInjection.cs
│   │   └── EventDispatcher.cs
│   ├── IntegrationEventHandlers
│   │   ├── Customers
│   │   │   ├── ...
│   │   │   └── SendWelcomeEmailHandler.cs
│   │   └── DependencyInjection.cs
│   ├── IntegrationEvents
│   │   └── Customers
│   │       ├── ...
│   │       └── IntegrationUserRegisteredEvent.cs
│   ├── JsonSerializers
│   │   ├── ...
│   │   └── OutboxMessagePayloadSerializer.cs
│   │  
│   └── DependencyInjection.cs
│
├── Options
│   ├── DependencyInjection.cs
│   ├── ...
│   ├── EmailOption.cs
│   ├── JwtOption.cs
│   ├── OtpOption.cs
│   ├── OutboxCleanupWorkerOption.cs
│   ├── OutboxWorkerOption.cs
│   ├── RandomPasswordOption.cs
│   ├── RedisOption.cs
│   └── RefreshTokenOption.cs
│
├── Persistence
│   ├── Database
│   │   ├── ApplicationDbContext.cs
│   │   │
│   │   ├── Configurations
│   │   │   ├── OutboxMessageConfiguration.cs
│   │   │   ├── UserConfiguration.cs
│   │   │   └── ...
│   │   ├── Entities
│   │   │   ├── OutboxMessage.cs
│   │   │   └── ...
│   │   └── Migrations
│   │       └── ...
│   ├── Repositories
│   │   ├── UnitWork.cs
│   │   ├── ...
│   │   └── UserRepository.cs
│   │
│   └── DependencyInjection.cs
│
├── Storage
│   ├── DependencyInjection.cs
│   ├── ...
│   └── LocalFileStorage.cs
│
├── Time
│   ├── DateTimeProvider.cs
│   ├── ...
│   └── DependencyInjection.cs
│
├── Workers
│   ├── DependencyInjection.cs
│   ├── ...
│   ├── OutboxWorker.cs
│   └── OutboxCleanupWorker.cs
│   
└── DependencyInjection.cs
```

# Patterns

- Repository
- Unit Of Work
- Outbox
- Observer