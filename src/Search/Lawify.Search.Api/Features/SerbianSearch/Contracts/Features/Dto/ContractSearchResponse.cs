using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.Dto;

public record ContractSearchResponse(
    List<ContractHitResponse> Hits,
    long NumberOfResults
);

public record ContractHitResponse(
    string? GovernmentName,
    string? GovernmentLevel,
    string? AgencySignatureName,
    string? AgencySignatureSurname,
    string? AgencySignatureFullName,
    string? Highlight,
    string? Content,
    Guid Id,
    string FileName,
    Metadata Metadata,
    string? GovernmentSignatureName,
    string? GovernmentSignatureSurname,
    string? GovernmentSignatureFullName,
    string? GovernmentPhone,
    string? GovernmentEmail,
    string? GovernmentAddress,
    GeoLocation? GeoLocation
);