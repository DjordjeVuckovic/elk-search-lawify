using Elastic.Clients.Elasticsearch;
using Lawify.Search.Api.Features.SerbianSearch.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Laws;

namespace Lawify.Search.Api.ElasticSearch.Mappings;

public static class SerbianMappingsExtensions
{
    public static void AddLawMappings(this ElasticsearchClientSettings settings, string indexName)
    {
        settings.DefaultMappingFor<Law>(sw =>
            sw
                .IndexName(indexName)
                .IdProperty(x => x.Id)
        );
    }

    public static void AddContractMappings(this ElasticsearchClientSettings settings, string indexName)
    {
        settings.DefaultMappingFor<Contract>(sc =>
            sc
                .IndexName(indexName)
                .IdProperty(x => x.Id)
        );
    }
}