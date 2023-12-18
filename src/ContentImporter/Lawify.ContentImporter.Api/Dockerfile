FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContentImporter/Lawify.ContentImporter.Api/Lawify.ContentImporter.Api.csproj", "ContentImporter/Lawify.ContentImporter.Api/"]
RUN dotnet restore "ContentImporter/Lawify.ContentImporter.Api/Lawify.ContentImporter.Api.csproj"
COPY . .
WORKDIR "/src/ContentImporter/Lawify.ContentImporter.Api"
RUN dotnet build "Lawify.ContentImporter.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lawify.ContentImporter.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lawify.ContentImporter.Api.dll"]
