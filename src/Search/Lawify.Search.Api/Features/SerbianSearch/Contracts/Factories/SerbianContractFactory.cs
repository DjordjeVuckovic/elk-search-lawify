using Lawify.Common.Common.PreProcessor;
using Lawify.Messaging.Events.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Types;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;

public class SerbianContractFactory
{
    public static SerbianContractIndex ToDomain(
        SerbianContractExported exported, GeoLocation? geoLocation)
    {
        var contract = new SerbianContractIndex {
            Id = SerbianContractIndex.CreateId(),
            GovernmentName = exported.Government?.Name,
            GovernmentAdministrationLevel = exported.Government?.AdministrationLevel,
            GovernmentPhone =  exported.Government?.Phone,
            GovernmentEmail = exported.Government?.Email,
            GovernmentCity = exported.Government?.Address?.City,
            GovernmentFullAddress = exported.Government?.Address?.FullAddress,
            GovernmentStreetNumber = exported.Government?.Address?.Number,
            GovernmentStreet = exported.Government?.Address?.Street,
            GovernmentSignatureName = exported.GovernmentSignature?.EmployeeName,
            GovernmentSignatureSurname = exported.GovernmentSignature?.EmployeeSurname,
            GovernmentSignatureFullName = exported.GovernmentSignature?.EmployeeFullName,
            AgencyPhone =  exported.Agency?.Phone,
            AgencyEmail = exported.Agency?.Email,
            AgencyStreet = exported.Agency?.Address?.Street,
            AgencyStreetNumber = exported.Agency?.Address?.Number,
            AgencyCity = exported.Agency?.Address?.City,
            AgencyFullAddress = exported.Agency?.Address?.FullAddress,
            AgencySignatureName = exported.AgencySignature?.EmployeeName,
            AgencySignatureSurname = exported.AgencySignature?.EmployeeSurname,
            AgencySignatureFullName = exported.AgencySignature?.EmployeeFullName,
            Signed = exported.CreatedAt,
            Content = exported.Content.PreProcessText(),
            Metadata = new Metadata(
                Title: exported.ContentMetadataExported.Title,
                FileName: exported.ContentMetadataExported.FileName,
                CreatedAt: exported.ContentMetadataExported.CreatedAt,
                Author: exported.ContentMetadataExported.Author,
                Category: exported.ContentMetadataExported.Category
            ),
            GeoLocation = geoLocation,
            FileName = exported.FileName
        };

        return contract;
    }
}