using CSharpFunctionalExtensions;
using Lawify.ContentDispatchingProcessor.Common.Files;

namespace Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors;

public interface IContentExtractor<T> where T : class
{
    Task<Result<T>> ExtractContentAsync(FileContent file, CancellationToken cancellationToken);
}