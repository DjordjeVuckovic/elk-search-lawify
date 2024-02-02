using CSharpFunctionalExtensions;
using Lawify.ContentDispatchingProcessor.Common.Files;

namespace Lawify.ContentDispatchingProcessor.ContentExtractors;

public interface IContentExtractor<T> where T : class
{
    public Task<Result<T>> ExtractContentAsync(FileContent file, CancellationToken cancellationToken);
}