using Lawify.Search.Api.Common.PreProcessor;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws;

public class SerbianLaw
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt  { get; set; }
    public string Content { get; set; } = null!;
    public IEnumerable<ArticleChunk> ArticleChunks { get; set; } = Enumerable.Empty<ArticleChunk>();
    public string? Title { get; set; }
    public string? Category { get; set; }
    public string FileName { get; set; } = null!;



    public static SerbianLaw Create(
        string content,string? title, string fileName, DateTime createdAt, string? category)
    {
        return new SerbianLaw {
            Id = Guid.NewGuid(),
            Content = content.PreProcessText(),
            Title = title?.PreProcessText(),
            FileName = fileName,
            CreatedAt = createdAt,
            Category = category
        };
    }
}