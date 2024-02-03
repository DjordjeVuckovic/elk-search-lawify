using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Lawify.Search.Api.Features.SerbianSearch.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Laws;
using GeoLocation = Lawify.Search.Api.Features.SerbianSearch.Shared.Types.GeoLocation;

namespace Lawify.Search.Api.ElasticSearch.Languages.Serbian;

public static class SerbianSearchExtensions
{
    public static void CreateSerbianContractIndex(this ElasticsearchClient client, string indexName)
    {
        client.Indices.Create<SerbianContract>(descriptor => descriptor
            .Index(indexName)
            .AddSerbianIndexSettings()
            .Mappings(
                m => m.Properties(p =>
                    p.GeoPoint(g => g.GeoLocation!)
                )
            )
        );
    }

    public static void CreateSerbianLawIndex(this ElasticsearchClient client, string indexName)
    {
        client.Indices.Create<SerbianLaw>(descriptor => descriptor
            .Index(indexName)
            .AddSerbianIndexSettings()
        );
    }

    public static void CreateDefaultSerbianIndex<T>(
        this ElasticsearchClient client,
        string indexName)
    {
        client.Indices.Create<T>(descriptor => descriptor
            .Index(indexName)
            .AddSerbianIndexSettings()
        );
    }

    private static CreateIndexRequestDescriptor<T> AddSerbianIndexSettings<T>(
        this CreateIndexRequestDescriptor<T> descriptor
    )
    {
        descriptor.Settings(s => s
            .Analysis(a => a
                .Analyzers(an => an
                    .Custom("serbian", ca => ca
                        .Tokenizer("standard")
                        .Filter(["lowercase", "serbian_stop", "serbian_normalization", "serbian_keywords", "serbian_stemmer"])
                    )
                )
                .TokenFilters(tf => tf
                    .Stop("serbian_stop", st =>
                        st.Stopwords(SerbianAnalyzer.StopWords)
                    )
                    .Stemmer("serbian_stemmer", st =>
                        st.Language("serbian")
                    )
                    .KeywordMarker("serbian_keywords", km =>
                        km.Keywords(SerbianAnalyzer.Keywords))
                    .PatternReplace("serbian_normalization", pr =>
                        pr.SerbianPatternTokenDescriptor()
                    )
                ).CharFilters(cf => cf
                    .PatternReplace("char_filter", pr => pr
                        .Pattern(@"(\d+)-(?=\d)")
                        .Replacement("$1_")
                    )
                )
            )
        );
        return descriptor;
    }
    private static void SerbianPatternTokenDescriptor(this PatternReplaceTokenFilterDescriptor descriptor)
    {
        descriptor
            .Pattern("č|ć")
            .Replacement("c")
            .Pattern("š")
            .Replacement("s")
            .Pattern("ž")
            .Replacement("z")
            .Pattern("đ")
            .Replacement("dj")
            .Pattern("dž")
            .Replacement("dz");
    }
}