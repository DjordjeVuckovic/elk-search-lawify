using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Laws;

public class Law
{
    public string Id { get; set; }
    public DateTime? Enacted  { get; set; }
    public string Content { get; set; } = null!;
    public GeoLocation? GeoLocation { get; set; }
    public static string CreateId() => Guid.NewGuid().ToString();

}