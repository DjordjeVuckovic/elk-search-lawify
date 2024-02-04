using CSharpFunctionalExtensions;
using Lawify.ContentDispatchingProcessor.Common.Files;
using Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Models;

namespace Lawify.ContentDispatchingProcessor.ProcessContent.ContentExtractors.Laws;

public class PdfLawExtractor(ILogger<PdfLawExtractor> logger) : IContentExtractor<LawExtracted>
{
    public async Task<Result<LawExtracted>> ExtractContentAsync(FileContent file, CancellationToken cancellationToken)
    {
        var tempFilePath = Path.GetTempFileName();
        try {
            await using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.ReadWrite)) {
                file.Content.Position = 0;
                await file.Content.CopyToAsync(fileStream, cancellationToken);
                await fileStream.FlushAsync(cancellationToken);
            }

            var pdfDocument = PdfDocument.FromFile(tempFilePath);
            var text = pdfDocument.ExtractAllText();
            var metadata = new ContentMetadata(
                pdfDocument.MetaData.Title,
                file.FileName,
                pdfDocument.MetaData.CreationDate,
                pdfDocument.MetaData.Author,
                null
            );

            logger.LogInformation("Pdf law extraction completed for {FileName} with lenght {@Info}",
                file.FileName,
                new FileInfo(tempFilePath).Length
            );

            return new LawExtracted(
                text,
                metadata
            );
        } catch (Exception ex) {
            logger.LogError(ex, "Error extracting pdf law for {FileName}", file.FileName);
            throw;
        } finally {
            // Clean up the temporary file
            if (File.Exists(tempFilePath)) {
                File.Delete(tempFilePath);
            }
        }
    }
}