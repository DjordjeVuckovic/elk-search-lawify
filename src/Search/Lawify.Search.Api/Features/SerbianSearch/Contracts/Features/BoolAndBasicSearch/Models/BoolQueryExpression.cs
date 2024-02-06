namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch.Models;

public class BoolQueryExpression : ISearchExpression
{
    public string SplitOperator { get; set; } = null!;
    public List<SearchExpression> BoolQueryFields { get; set; } = null!;
}