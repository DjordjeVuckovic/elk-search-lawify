using Lawify.Common.Common.PreProcessor;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws;

public class SerbianLaw
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public IEnumerable<ArticleChunk> ArticleChunks { get; set; } = Enumerable.Empty<ArticleChunk>();

    public Metadata Metadata { get; set; } = null!;



    public static SerbianLaw Create(
        string content,string? title, string? fileName, DateTime createdAt,string? author, string? category)
    {
        return new SerbianLaw {
            Id = Guid.NewGuid(),
            Content = content.PreProcessText(),
            Metadata = new Metadata(title?.PreProcessText(), fileName, createdAt,author, category)
        };
    }
}