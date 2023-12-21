using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws;

public class SerbianLaw
{
    public Guid Id { get; set; }
    public DateTime? Enacted  { get; set; }
    public string Content { get; set; } = null!;
    public IEnumerable<ArticleChunk> ArticleChunks { get; set; } = Enumerable.Empty<ArticleChunk>();
    public string? Title { get; set; } = null!;
    public string? Category { get; set; } = null!;
    public GeoLocation? GeoLocation { get; set; }
}