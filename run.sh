source path.sh

dotnet build

sudo systemctl start redis.service

dotnet run --project $api_proj