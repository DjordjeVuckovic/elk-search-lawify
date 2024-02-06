using Elastic.Clients.Elasticsearch;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws.CreateIndex;

public static class CreateSerbianLawIndexExtension
{
    public static void CreateSerbianLawIndex(this ElasticsearchClient client, string indexName)
    {
        client.Indices.Create<SerbianLawIndex>(descriptor => descriptor
            .Index(indexName)
            .Mappings(m =>
                m.Properties(x =>
                    x.Text(sl => sl.Content, propertyDescriptor =>
                        propertyDescriptor
                            .Analyzer("serbian")
                            .SearchAnalyzer("serbian")
                    ).Keyword(kw => kw.Id,
                        propertyDescriptor => propertyDescriptor.IgnoreAbove(32)
                    ).Keyword(kw => kw.FileName,
                        propertyDescriptor => propertyDescriptor.IgnoreAbove(265)
                    )))
        );
    }
}