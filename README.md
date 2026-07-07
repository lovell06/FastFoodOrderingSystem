# Fast Food Ordering System

### Required dotnet sdk 10.0
```bash
dotnet --version
```

### Install dotnet ef tool
```bash
dotnet tool install --global dotnet-ef
```

### Install Project's Dependences
```bash
dotnet restore
```

### Enviroment Variable (User secrets)
```bash
cd src/FastFoodOrderingSystem.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-db-connection-string"
dotnet user-secrets set "JwtOption:Key" "your-key"
dotnet user-secrets set "OtpOption:SecretKey" "your-secret-key"
dotnet user-secrets set "EmailOption:UserName" "your-email"
dotnet user-secrets set "EmailOption:Password" "your-password"
cd ../../
```

### Migration Command
```bash
dotnet ef database update --project src/FastFoodOrderingSystem.Infrastructure/FastFoodOrderingSystem.Infrastructure.csproj
```

### Cache Service Command (This project use Redis, Ensure redis server is installed)
```bash Run this command
redis-server
```

### Build & Run Project
```bash
dotnet build
dotnet run --project src/FastFoodOrderingSystem.Api/FastFoodOrderingSystem.Api.csproj
```
