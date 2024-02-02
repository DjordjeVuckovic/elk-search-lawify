FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContentImporter/ThirdPartyImporters/Lawify.ThirdPartyImport/Lawify.ThirdPartyImport.csproj", "ContentImporter/ThirdPartyImporters/Lawify.ThirdPartyImport/"]
RUN dotnet restore "ContentImporter/ThirdPartyImporters/Lawify.ThirdPartyImport/Lawify.ThirdPartyImport.csproj"
COPY . .
WORKDIR "/src/ContentImporter/ThirdPartyImporters/Lawify.ThirdPartyImport"
RUN dotnet build "Lawify.ThirdPartyImport.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lawify.ThirdPartyImport.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lawify.ThirdPartyImport.dll"]
