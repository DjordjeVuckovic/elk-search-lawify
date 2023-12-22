using Lawify.Search.Api.Features.SerbianSearch.Contracts.Types;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts;

public record Contract
{
    public string Id { get; set; }
    public Government? Government { get; set; }
    public Signature? GovernmentSignature { get; set; }
    public Agency? Agency { get; set; }
    public Signature? AgencySignature { get; set; }
    public DateTime? Signed { get; set; }
    public string Content { get; set; } = null!;
    public GeoLocation? GeoLocation { get; set; }

    public static string CreateId() => Guid.NewGuid().ToString();
}