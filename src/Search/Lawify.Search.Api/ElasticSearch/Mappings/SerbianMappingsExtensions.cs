using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.Features.SerbianSearch.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Laws;

namespace Lawify.Search.Api.ElasticSearch.Mappings;

public static class SerbianMappingsExtensions
{
    public static void AddSerbianLawMappings(this ElasticsearchClientSettings settings, string indexName)
    {
        settings.DefaultMappingFor<SerbianLaw>(sw =>
            sw
                .IndexName(indexName)
                .IdProperty(x => x.Id)
        );
    }

    public static void AddSerbianContractMappings(this ElasticsearchClientSettings settings, string indexName)
    {
        settings.DefaultMappingFor<SerbianContract>(sc =>
            sc
                .IndexName(indexName)
                .IdProperty(x => x.Id)
        );
    }
}