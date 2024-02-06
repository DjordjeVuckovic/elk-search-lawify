using Lawify.Search.Api.LocationSearch.HttpHandler;
using HttpClientHandler = Lawify.Search.Api.LocationSearch.HttpHandler.HttpClientHandler;

namespace Lawify.Search.Api.LocationSearch.LocationIq;

public static class LocationIqExtension
{
    public static void AddLocationIq(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LocationIqConfig>(configuration.GetSection(LocationIqConfig.SectionName))
            .AddScoped<ILocationClient, LocationIqClient>()
            .AddScoped<IHttpClientHandler, HttpClientHandler>();
    }
}