using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Lawify.Search.Api.Features.SerbianSearch.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Laws;
using GeoLocation = Lawify.Search.Api.Features.SerbianSearch.Shared.Types.GeoLocation;

namespace Lawify.Search.Api.ElasticSearch.Languages.Serbian;

public static class SerbianSearchExtensions
{
    public static void CreateSerbianContractIndex(this ElasticsearchClient client, string indexName)
    {
        client.Indices.Create<SerbianContractIndex>(descriptor => descriptor
            .Index(indexName)
            .Mappings(
                m => m.Properties(p => {
                            p.GeoPoint(g => g.GeoLocation!);
                            p.Text(x => x.Content, propertyDescriptor => {
                                propertyDescriptor.Analyzer("serbian");
                                propertyDescriptor.SearchAnalyzer("serbian");
                            });
                            p.Date(x => x.Signed!);
                            p.Text(t => t.GovernmentSignatureName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentSignatureSurname!, v => v
                                        .IgnoreAbove(265)
                                        .Suffix("keyword")))
                                .SearchAnalyzer("serbian"));
                            p.Text(t => t.GovernmentSignatureSurname!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentSignatureSurname!, v => v
                                        .IgnoreAbove(265)
                                        .Suffix("keyword")))
                                .SearchAnalyzer("serbian"));
                            p.Text(t => t.GovernmentSignatureFullName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentSignatureFullName!, v => v
                                        .IgnoreAbove(265)
                                        .Suffix("keyword")))
                                .SearchAnalyzer("serbian"));
                            p.Text(t => t.GovernmentName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentName!, v => v
                                        .IgnoreAbove(265)
                                        .Suffix("keyword")))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.AgencySignatureName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.AgencySignatureName!, v => v
                                        .IgnoreAbove(265)
                                        .Suffix("keyword")))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.AgencySignatureSurname!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.AgencySignatureSurname!, v => v
                                        .IgnoreAbove(265)
                                        .Suffix("keyword")))
                                .SearchAnalyzer("serbian"));
                        }
                    )
                    .Enabled()
            )
        );
    }

    public static void CreateSerbianLawIndex(this ElasticsearchClient client, string indexName)
    {
        client.Indices.Create<SerbianLawIndex>(descriptor => descriptor
            .Index(indexName)
            .Settings(x =>
                x.Analysis(a => {
                    a.Analyzers(ad => ad.Keyword("serbian"));
                    a.TokenFilters(tf =>
                        tf.Stemmer("serbian_stemmer",
                            st => st.Language("serbian"))
                    );
                }))
            .Mappings(m =>
                m.Properties(x => x.Keyword(lw => lw.Content, propertyDescriptor => propertyDescriptor.IndexOptions(new IndexOptions())))
            )
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