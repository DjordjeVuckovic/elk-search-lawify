namespace Lawify.Search.Api.Features.SerbianSearch.Contracts.Features.BoolAndBasicSearch.Models;

public class SearchExpression
{
    public SearchLeaf? SearchLeaf { get; set; }
    public BoolQueryExpression BoolQueryExpression { get; set; } = null!;

    public ISearchExpression GetExpressionType()
    {
        if (SearchLeaf is not null)
            return SearchLeaf;
        return BoolQueryExpression;
    }
}