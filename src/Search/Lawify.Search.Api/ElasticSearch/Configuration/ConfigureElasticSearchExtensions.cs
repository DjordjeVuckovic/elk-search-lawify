using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.ElasticSearch.Languages.Serbian;
using Lawify.Search.Api.ElasticSearch.Mappings;
using Lawify.Search.Api.Features.SerbianSearch.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Laws;
using Microsoft.Extensions.Options;

namespace Lawify.Search.Api.ElasticSearch.Configuration;

public static class ConfigureElasticSearchExtensions
{
    private const int TimeoutMinutes = 2;

    public static IServiceCollection AddElasticSearch(this IServiceCollection services)
    {
        var elkOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ElasticSearchOptions>>().Value;

        var settings = new ElasticsearchClientSettings(new Uri(elkOptions.Uri))
            .PrettyJson();

        var serbianLawIndex = elkOptions.SerbianLawsIndex;
        var serbianContractIndex = elkOptions.SerbianContractsIndex;
        settings.AddAllMappings(serbianLawIndex, serbianContractIndex);

        var client = new ElasticsearchClient(settings);
        services.AddSingleton(client);

        client.CreateLawIndex(serbianLawIndex);
        client.CreateContractIndex(serbianContractIndex);

        return services;
    }
    private static void AddAllMappings(this ElasticsearchClientSettings settings, string serbianLawIndex, string serbianContractIndex)
    {
        settings.AddLawMappings(serbianLawIndex);
        settings.AddContractMappings(serbianContractIndex);
    }

}