using Elastic.Clients.Elasticsearch;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.CreateIndex;

public static class CreateSerbianContractIndexExtension
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

                            p.Keyword(x => x.Id,
                                propertyDescriptor => propertyDescriptor.IgnoreAbove(32)
                            );
                            p.Keyword(x => x.FileName,
                                propertyDescriptor => propertyDescriptor.IgnoreAbove(265)
                            );
                            p.Date(x => x.Signed!);

                            p.Text(t => t.GovernmentSignatureName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                            k.GovernmentSignatureName!, v => v
                                            .IgnoreAbove(265)
                                    )
                                )
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.GovernmentSignatureSurname!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentSignatureSurname!, v => v
                                        .IgnoreAbove(265)))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.GovernmentSignatureFullName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentSignatureFullName!, v => v
                                        .IgnoreAbove(265)))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.GovernmentAddress!, c => c
                                .Analyzer("serbian")
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.GovernmentName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentName!, v => v
                                        .IgnoreAbove(265)))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.GovernmentLevel!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.GovernmentLevel!, v => v
                                        .IgnoreAbove(265)))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.AgencySignatureName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.AgencySignatureName!, v => v
                                        .IgnoreAbove(265)))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.AgencySignatureSurname!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.AgencySignatureSurname!, v => v
                                        .IgnoreAbove(265)))
                                .SearchAnalyzer("serbian"));

                            p.Text(t =>
                                t.AgencySignatureFullName!, c => c
                                .Analyzer("serbian")
                                .Fields(x =>
                                    x.Keyword(k =>
                                        k.AgencySignatureFullName!, v => v
                                        .IgnoreAbove(265)))
                                .SearchAnalyzer("serbian"));

                            p.Text(t => t.AgencyAddress!, c => c
                                .Analyzer("serbian")
                                .SearchAnalyzer("serbian"));
                        }
                    )
                    .Enabled()
            )
        );
    }
}