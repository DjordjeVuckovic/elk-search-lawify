namespace Lawify.ContentImporter.Api.DependencyInjection.Api;

internal static class ApiExtensions
{
    internal static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCors(options => {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
        });
        return services;
    }
}