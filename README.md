# Fast Food Ordering System



## Architecture

Architecture: Modular monolith (Clean architecture + DDD + CQRS)

Design pattern: Repository Pattern, Decorator Pattern

## Implementation

#### Required dotnet sdk 10.0
```bash
dotnet --version
```

#### Install dotnet ef tool
```bash
dotnet tool install --global dotnet-ef
```

#### Install Project's Dependences
```bash
dotnet restore
```

#### Database Management System: PostgreSQL (Ensure installed)

#### Enviroment Variable (User secrets: pls pass your secret configuration to "your-key", "your-email",...)
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-db-connection-string" --project src/*.Api/*.csproj
dotnet user-secrets set "JwtOption:Key" "your-key" --project src/*.Api/*.csproj
dotnet user-secrets set "OtpOption:SecretKey" "your-secret-key" --project src/*.Api/*.csproj
dotnet user-secrets set "EmailOption:UserName" "your-email" --project src/*.Api/*.csproj
dotnet user-secrets set "EmailOption:Password" "your-password" --project src/*.Api/*.csproj
```

#### Migration Command
```bash
dotnet ef database update --project src/*.Infrastructure/*.csproj
```

#### Cache Service Command (This project use Redis, Ensure redis server is installed)
```bash Run this command
redis-server
```

#### Build & Run Project
```bash
dotnet build
dotnet run --project src/*.Api/*.csproj
```

#### This project use Scalar (API documentation)
```bash
URL: host:port/scalar
Ex URL: localhost:5209/scalar
```
