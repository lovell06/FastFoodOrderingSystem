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
dotnet user-secrets init --project src/*.Api/*.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-db-connection-string" --project src/*.Api/*.csproj
dotnet user-secrets set "JwtOption:Key" "your-key" --project src/*.Api/*.csproj
dotnet user-secrets set "OtpOption:SecretKey" "your-secret-key" --project src/*.Api/*.csproj
dotnet user-secrets set "EmailOption:UserName" "your-email" --project src/*.Api/*.csproj
dotnet user-secrets set "EmailOption:Password" "your-password" --project src/*.Api/*.csproj
```

### Migration Command
```bash
dotnet ef database update --project src/*.Infrastructure/*.csproj
```

### Cache Service Command (This project use Redis, Ensure redis server is installed)
```bash Run this command
redis-server
```

### Build & Run Project
```bash
dotnet build
dotnet run --project src/*.Api/*.csproj
```
