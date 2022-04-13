FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MatchMake.Backend.csproj", "."]
RUN dotnet restore "MatchMake.Backend.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MatchMake.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MatchMake.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MatchMake.Backend.dll"]