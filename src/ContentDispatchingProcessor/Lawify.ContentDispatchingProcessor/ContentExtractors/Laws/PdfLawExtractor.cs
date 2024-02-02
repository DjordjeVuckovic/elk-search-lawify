using CSharpFunctionalExtensions;
using Lawify.ContentDispatchingProcessor.Common.Files;

namespace Lawify.ContentDispatchingProcessor.ContentExtractors.Laws;

public class PdfLawExtractor : IContentExtractor<Law>
{
    private ILogger<PdfLawExtractor> _logger;
    public PdfLawExtractor(ILogger<PdfLawExtractor> logger)
    {
        _logger = logger;
    }
    public async Task<Result<Law>> ExtractContentAsync(FileContent file, CancellationToken cancellationToken)
    {
        var tempFilePath = Path.GetTempFileName();
        try
        {
            await using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                file.Content.Position = 0;
                await file.Content.CopyToAsync(fileStream, cancellationToken);
                await fileStream.FlushAsync(cancellationToken);
            }
            var text = await Task.Run(() =>
            {
                var pdfDocument = PdfDocument.FromFile(tempFilePath);
                return pdfDocument.ExtractAllText();
            }, cancellationToken);

            _logger.LogInformation("Pdf law extraction completed for {FileName} with {@Info}", file.FileName, new FileInfo(tempFilePath));

            return new Law(text);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error extracting pdf law for {FileName}", file.FileName);
            throw;
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }

    }

}