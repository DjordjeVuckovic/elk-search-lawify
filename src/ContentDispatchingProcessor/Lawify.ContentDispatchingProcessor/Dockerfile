FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor.csproj", "ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor/"]
#common
COPY ["src/Common/Lawify.Common/Lawify.Common.csproj", "Common/Lawify.Common/"]
COPY ["src/Common/Lawify.Messaging.Events/Lawify.Messaging.Events.csproj", "Common/Lawify.Messaging.Events/"]

RUN dotnet restore "ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor.csproj"

COPY ["src/ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor/", "ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor/"]
#common
COPY ["src/Common/Lawify.Common/", "Common/Lawify.Common/"]
COPY ["src/Common/Lawify.Messaging.Events/", "Common/Lawify.Messaging.Events/"]

WORKDIR "/src/ContentDispatchingProcessor/Lawify.ContentDispatchingProcessor"
RUN dotnet build "Lawify.ContentDispatchingProcessor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lawify.ContentDispatchingProcessor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lawify.ContentDispatchingProcessor.dll"]
