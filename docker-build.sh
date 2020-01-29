cd src
docker build -f ./MicroServices.Api/Dockerfile -t microservices.api ./MicroServices.Api
docker build -f ./MicroServices.Services.Activities/Dockerfile -t microservices.services.activities ./MicroServices.Services.Activities
docker build -f ./MicroServices.Services.Identity/Dockerfile -t microservices.services.identity ./MicroServices.Services.Identity