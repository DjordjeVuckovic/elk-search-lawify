using System.Text.RegularExpressions;

namespace Lawify.Common.Common.PreProcessor;

public static partial class PreProcessExtensions
{
    public static string PreProcessWithSpecialChars(this string content)
    {
        // var withoutSpecialChars = SpecialCharactersRegex().Replace(
        //     content.Trim(), " ");

        // var normalizedSpaces = SingleSpace()
        //     .Replace(content.Trim(), " ");

        return content;
    }
    public static string PreProcessText(this string content)
    {
        // var withoutHtml = HtmlCharRegex().Replace(content, string.Empty);
        //
        // var decodedHtml = System.Net.WebUtility.HtmlDecode(withoutHtml);

        return content.PreProcessWithSpecialChars();
    }

    [GeneratedRegex("<.*?>")]
    private static partial Regex HtmlCharRegex();

    [GeneratedRegex(@"\t|\n|\r|\s+|[^a-zA-Z0-9\s]")]
    private static partial Regex SpecialCharactersRegex();
    [GeneratedRegex(@"\s+")]
    private static partial Regex SingleSpace();
}