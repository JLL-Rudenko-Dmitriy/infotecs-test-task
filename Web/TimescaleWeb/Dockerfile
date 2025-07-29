FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web/TimescaleWeb/TimescaleWeb.csproj", "Web/TimescaleWeb/"]
COPY ["Application/TimescaleApplication/TimescaleApplication.csproj", "Application/TimescaleApplication/"]
COPY ["Core/TimescaleDomain/TimescaleDomain.csproj", "Core/TimescaleDomain/"]
COPY ["Infrastructure/Persistence/Persistence.csproj", "Infrastructure/Persistence/"]
COPY ["Infrastructure/StringCoder/StringCoder.csproj", "Infrastructure/StringCoder/"]
RUN dotnet restore "Web/TimescaleWeb/TimescaleWeb.csproj"
COPY . .
WORKDIR "/src/Web/TimescaleWeb"
RUN dotnet build "./TimescaleWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TimescaleWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimescaleWeb.dll"]
