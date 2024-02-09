FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiGateway/Lawify.ApiGateway/Lawify.ApiGateway.csproj", "ApiGateway/Lawify.ApiGateway/"]
RUN dotnet restore "ApiGateway/Lawify.ApiGateway/Lawify.ApiGateway.csproj"
COPY ["src/ApiGateway/Lawify.ApiGateway/", "ApiGateway/Lawify.ApiGateway/"]

WORKDIR "/src/ApiGateway/Lawify.ApiGateway"
RUN dotnet build "Lawify.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lawify.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lawify.ApiGateway.dll"]
