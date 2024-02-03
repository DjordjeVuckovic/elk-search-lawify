using System.Text.RegularExpressions;

namespace Lawify.Search.Api.Common.PreProcessor;

public static partial class PreProcessExtensions
{
    public static string PreProcessText(this string content)
    {
        var withoutHtml = HtmlCharRegex().Replace(content, string.Empty);

        var decodedHtml = System.Net.WebUtility.HtmlDecode(withoutHtml);

        // Replace newline, carriage return, and tab characters with space
        var withoutSpecialChars = SpecialCharactersRegex().Replace(
            decodedHtml, " ");

        return withoutSpecialChars;
    }

    [GeneratedRegex("<.*?>")]
    private static partial Regex HtmlCharRegex();

    [GeneratedRegex(@"\t|\n|\r|\s+|[^a-zA-Z0-9\s]")]
    private static partial Regex SpecialCharactersRegex();
}