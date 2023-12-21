namespace Lawify.Search.Api.ElasticSearch.Languages.Serbian;

public class SerbianAnalyzer
{
    public static readonly string[] StopWords = {"bio"};
    public static readonly string[] Keywords = {"ć", "č", "đ", "š", "ž", "lj", "nj", "dž"};
}