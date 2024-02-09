FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Search/Lawify.Search.Api/Lawify.Search.Api.csproj", "Search/Lawify.Search.Api/"]
#common
COPY ["src/Common/Lawify.Common/Lawify.Common.csproj", "Common/Lawify.Common/"]
COPY ["src/Common/Lawify.Messaging.Events/", "Common/Lawify.Messaging.Events/"]
RUN dotnet restore "Search/Lawify.Search.Api/Lawify.Search.Api.csproj"

COPY ["src/Search/Lawify.Search.Api/", "Search/Lawify.Search.Api/"]
#common
COPY ["src/Common/Lawify.Common/", "Common/Lawify.Common/"]
COPY ["src/Common/Lawify.Messaging.Events/", "Common/Lawify.Messaging.Events/"]

WORKDIR "/src/Search/Lawify.Search.Api"
RUN dotnet build "Lawify.Search.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lawify.Search.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lawify.Search.Api.dll"]
