namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch.Models;

public class SearchLeaf : ISearchExpression
{
    public  string Field { get; set; } = null!;
    public  string Value { get; set; } = null!;
    public  bool IsPhrase { get; set; }
}