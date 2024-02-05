using Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Contracts.Models;
using Lawify.Messaging.Events.Contents;
using Lawify.Messaging.Events.Contracts;

namespace Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Contracts.Factories;

public class ContractFactory
{
    public static SerbianContractExported CreateContractExported(
        ContractExtracted extracted, string fileName)
    {
        var exported = new SerbianContractExported {
            Government = new GovernmentExported(
                Name: extracted.ClientName,
                AdministrationLevel: extracted.ClientLevel,
                Phone: extracted.ClientPhoneNumber,
                Email: extracted.ClientEmail,
                Address: new AddressExported(
                    Street: extracted.ClientAddress?.Street,
                    Number: extracted.ClientAddress?.Number,
                    City: extracted.ClientAddress?.City,
                    FullAddress: extracted.ClientAddress?.FullAddress
                )
            ),
            GovernmentSignature = new SignatureExported(
                EmployeeName: extracted.ClientSign?.EmployeeName,
                EmployeeSurname: extracted.ClientSign?.EmployeeSurname,
                EmployeeFullName: extracted.ClientSign?.EmployeeFullName
            ),
            Agency = new AgencyExported(
                Phone: extracted.AgencyPhoneNumber,
                Email: extracted.AgencyEmail,
                Address: new AddressExported(
                    Street: extracted.AgencyAddress?.Street,
                    Number: extracted.AgencyAddress?.Number,
                    City: extracted.AgencyAddress?.City,
                    FullAddress: extracted.AgencyAddress?.FullAddress
                )
            ),
            AgencySignature = new SignatureExported(
                EmployeeName: extracted.AgencySign?.EmployeeName,
                EmployeeSurname: extracted.AgencySign?.EmployeeSurname,
                EmployeeFullName: extracted.AgencySign?.EmployeeFullName
            ),
            CreatedAt = extracted.CreatedAt,
            Content = extracted.Content,
            ContentMetadataExported = new ContentMetadataExported(
                Title: extracted.Metadata?.Title,
                FileName: extracted.Metadata?.FileName,
                CreatedAt: extracted.Metadata?.CreatedAt ?? default(DateTime),
                Author: extracted.Metadata?.Author,
                Category: extracted.Metadata?.Category
            ),
            FileName = fileName
        };

        return exported;
    }
}
