FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor.Api/Lawify.ContentDispatchingProcessor.Api.csproj", "ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor.Api/"]
RUN dotnet restore "ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor.Api/Lawify.ContentDispatchingProcessor.Api.csproj"
COPY . .
WORKDIR "/src/ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor.Api"
RUN dotnet build "Lawify.ContentDispatchingProcessor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lawify.ContentDispatchingProcessor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lawify.ContentDispatchingProcessor.Api.dll"]
