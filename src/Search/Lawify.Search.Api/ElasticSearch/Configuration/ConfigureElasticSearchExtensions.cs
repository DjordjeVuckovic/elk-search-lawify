using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.ElasticSearch.Languages.Serbian;
using Lawify.Search.Api.ElasticSearch.Mappings;
using Lawify.Search.Api.Features.SerbianSearch.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Laws;
using Microsoft.Extensions.Options;
using Serilog;

namespace Lawify.Search.Api.ElasticSearch.Configuration;

public static class ConfigureElasticSearchExtensions
{
    private const int TimeoutMinutes = 2;

    public static IServiceCollection AddElasticSearch(this IServiceCollection services)
    {
        var elkOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ElasticSearchOptions>>().Value;

        var settings = new ElasticsearchClientSettings(new Uri(elkOptions.Uri))
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(TimeoutMinutes))
            //development only
            .DisableDirectStreaming(false)
            .EnableDebugMode(x => {
                Log.Information("Elasticsearch request: {@RequestBody}", x);
            });

        var serbianLawIndex = elkOptions.SerbianLawsIndex;
        var serbianContractIndex = elkOptions.SerbianContractsIndex;

        settings.AddSerbianLawMappings(serbianLawIndex);
        settings.AddSerbianContractMappings(serbianContractIndex);

        var client = new ElasticsearchClient(settings);
        services.AddSingleton(client);

        client.CreateSerbianLawIndex(serbianLawIndex);
        client.CreateSerbianContractIndex(serbianContractIndex);

        return services;
    }

}