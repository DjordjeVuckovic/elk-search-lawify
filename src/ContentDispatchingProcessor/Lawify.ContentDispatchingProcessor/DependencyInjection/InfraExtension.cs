using System.Diagnostics.Contracts;
using Lawify.Content.Api.Configuration;
using Lawify.ContentDispatchingProcessor.Configuration;
using Lawify.ContentDispatchingProcessor.ContentExtractors;
using Lawify.ContentDispatchingProcessor.ContentExtractors.Contracts;
using Lawify.ContentDispatchingProcessor.ContentExtractors.Laws;
using Lawify.ContentDispatchingProcessor.MessageBroker.Extensions;
using Minio;
using Contract = Lawify.ContentDispatchingProcessor.ContentExtractors.Contracts.Contract;

namespace Lawify.ContentDispatchingProcessor.DependencyInjection;

public static class InfraExtension
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AddIronPdfLicence(configuration);

        services
            .AddMessageBroker()
            .AddMinio(configuration)
            .AddScoped<IContentExtractor<Law>, PdfLawExtractor>()
            .AddScoped<IContentExtractor<Contract>, PdfContractExtractor>();

        return services;
    }

    private static void AddIronPdfLicence(IConfiguration configuration)
    {
        var options = configuration
            .GetSection(PdfOptions.Pdf)
            .Get<PdfOptions>()!;

        License.LicenseKey = options.License;
    }

    private static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinio(x => {
            var options = configuration
                .GetSection(MinioOptions.Minio)
                .Get<MinioOptions>()!;
            x.WithCredentials(options.AccessKey, options.SecretKey);
            x.WithEndpoint(options.Endpoint);
            x.WithSSL(false);
        });
        return services;
    }
}