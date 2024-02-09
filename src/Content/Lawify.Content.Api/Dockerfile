FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN ls
COPY ["src/Content/Lawify.Content.Api/Lawify.Content.Api.csproj", "Content/Lawify.Content.Api/"]
#common
COPY ["src/Common/Lawify.Common/Lawify.Common.csproj", "Common/Lawify.Common/"]
COPY ["src/Common/Lawify.Messaging.Events/", "Common/Lawify.Messaging.Events/"]

RUN dotnet restore "Content/Lawify.Content.Api/Lawify.Content.Api.csproj"

COPY ["src/Content/Lawify.Content.Api/", "Content/Lawify.Content.Api/"]
##common
COPY ["src/Common/Lawify.Common/", "Common/Lawify.Common/"]
COPY ["src/Common/Lawify.Messaging.Events/", "Common/Lawify.Messaging.Events/"]

WORKDIR "/src/Content/Lawify.Content.Api"
RUN dotnet build "Lawify.Content.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lawify.Content.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lawify.Content.Api.dll"]
