using Lawify.Search.Api.Features.SerbianSearch.Contracts.Types;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts;

public record SerbianContractIndex
{
    public Guid Id { get; set; }
    public DateTime? Signed { get; set; }
    public string Content { get; set; } = null!;
    public GeoLocation? GeoLocation { get; set; }
    public Metadata Metadata { get; set; } = null!;
    public string FileName { get; set; } = null!;

    #region GovernmentAddress
    public string? GovernmentStreet{ get; set; }
    public string? GovernmentStreetNumber { get; set; }
    public string? GovernmentCity{ get; set; }
    public string? GovernmentFullAddress { get; set; }
    #endregion

    #region GovernmentInfo
    public string? GovernmentAdministrationLevel { get; set; }
    public string? GovernmentName { get; set; }
    public string? GovernmentPhone { get; set; }
    public string? GovernmentEmail { get; set; }
    public string? GovernmentSignatureName { get; set; }
    public string? GovernmentSignatureSurname { get; set; }
    public string? GovernmentSignatureFullName { get; set; }
    #endregion

    #region AgencyAddress
    public string? AgencyStreet { get; set; }
    public string? AgencyStreetNumber { get; set; }
    public string? AgencyCity { get; set; }
    public string? AgencyFullAddress { get; set; }
    #endregion

    #region AgencyInfo
    public string? AgencyPhone { get; set; }
    public string? AgencyEmail { get; set; }
    public string? AgencySignatureName { get; set; }
    public string? AgencySignatureSurname { get; set; }
    public string? AgencySignatureFullName { get; set; }
    #endregion

    public static Guid CreateId() => Guid.NewGuid();


}