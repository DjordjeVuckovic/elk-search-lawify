using CSharpFunctionalExtensions;
using Lawify.ContentDispatchingProcessor.Common.Files;

namespace Lawify.ContentDispatchingProcessor.ContentExtractors.Contracts;

public class PdfContractExtractor : IContentExtractor<Contract>
{
    public Task<Result<Contract>> ExtractContentAsync(FileContent file, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}