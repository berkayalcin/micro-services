cd ../src
dotnet publish ./MicroServices.Api -c Release -o ./bin/Docker
dotnet publish ./MicroServices.Services.Activities -c Release -o ./bin/Docker
dotnet publish ./MicroServices.Services.Identity -c Release -o ./bin/Docker