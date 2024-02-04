using Lawify.Search.Api.Features.SerbianSearch.Contracts.Types;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts;

public record SerbianContract
{
    public Guid Id { get; set; }
    public Government? Government { get; set; }
    public Address? GovernmentAddress { get; set; }
    public Signature? GovernmentSignature { get; set; }
    public Agency? Agency { get; set; }
    public Address? AgencyAddress { get; set; }
    public Signature? AgencySignature { get; set; }
    public DateTime? Signed { get; set; }
    public string Content { get; set; } = null!;
    public GeoLocation? GeoLocation { get; set; }
    public Metadata Metadata { get; set; } = null!;
    public string FileName { get; set; } = null!;

    public static Guid CreateId() => Guid.NewGuid();

    public static string CreateIdAsString(Guid id) => Guid.NewGuid().ToString();

    public static Guid IdFromString(string id) => Guid.Parse(id);
}