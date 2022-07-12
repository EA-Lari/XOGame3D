# FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build-env
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out
# mcr.microsoft.com/dotnet/aspnet:6.0
# FROM mcr.microsoft.com/dotnet/runtime:6.0-bullseye-slim
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "MatchMake.Backend.dll"]