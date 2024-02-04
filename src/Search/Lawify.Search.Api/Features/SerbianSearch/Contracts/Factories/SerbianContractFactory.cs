using Lawify.Common.Common.PreProcessor;
using Lawify.Messaging.Events.Contracts;
using Lawify.Search.Api.Features.SerbianSearch.Contracts.Types;
using Lawify.Search.Api.Features.SerbianSearch.Shared.Types;

namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Factories;

public class SerbianContractFactory
{
    public static SerbianContract ToDomain(
        SerbianContractExported exported, GeoLocation? geoLocation)
    {
        var contract = new SerbianContract {
            Id = SerbianContract.CreateId(),
            Government = exported.Government is not null
                ? new Government(
                    Name: exported.Government?.Name,
                    AdministrationLevel: exported.Government?.AdministrationLevel,
                    Phone: exported.Government?.Phone,
                    Email: exported.Government?.Email
                )
                : null,
            GovernmentAddress = new Address(
                Street: exported.Government?.Address?.Street,
                Number: exported.Government?.Address?.Number,
                City: exported.Government?.Address?.City
                )
            ,
            GovernmentSignature = exported.GovernmentSignature != null
                ? new Signature(
                    EmployeeName: exported.GovernmentSignature.EmployeeName,
                    EmployeeSurname: exported.GovernmentSignature.EmployeeSurname,
                    EmployeeFullName: exported.GovernmentSignature.EmployeeFullName
                )
                : null,
            Agency = new Agency(
                Phone: exported.Agency?.Phone,
                Email: exported.Agency?.Email
            ),
            AgencyAddress = new Address(
                Street: exported.Agency?.Address?.Street,
                Number: exported.Agency?.Address?.Number,
                City: exported.Agency?.Address?.City
            ),
            AgencySignature = exported.AgencySignature != null
                ? new Signature(
                    EmployeeName: exported.AgencySignature.EmployeeName,
                    EmployeeSurname: exported.AgencySignature.EmployeeSurname,
                    EmployeeFullName: exported.AgencySignature.EmployeeFullName
                )
                : null,
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