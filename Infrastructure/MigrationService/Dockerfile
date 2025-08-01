﻿FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Infrastructure/MigrationService/MigrationService.csproj", "Infrastructure/MigrationService/"]
COPY ["Infrastructure/Persistence/Persistence.csproj", "Infrastructure/Persistence/"]
COPY ["Core/TimescaleDomain/TimescaleDomain.csproj", "Core/TimescaleDomain/"]
COPY ["Infrastructure/StringCoder/StringCoder.csproj", "Infrastructure/StringCoder/"]
RUN dotnet restore "Infrastructure/MigrationService/MigrationService.csproj"
COPY . .
WORKDIR "/src/Infrastructure/MigrationService"
RUN dotnet build "./MigrationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MigrationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
    
ENTRYPOINT ["dotnet", "MigrationService.dll"]