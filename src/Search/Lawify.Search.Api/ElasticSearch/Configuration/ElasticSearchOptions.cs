using Microsoft.Extensions.Options;

namespace Lawify.Search.Api.ElasticSearch.Configuration;

public class ElasticSearchOptions : IValidateOptions<ElasticSearchOptions>
{
    public const string Els = "ELS";
    public string Uri { get; init; } = string.Empty;
    public string SerbianLawsIndex { get; init; } = string.Empty;
    public string SerbianContractsIndex { get; init; } = string.Empty;
    public ValidateOptionsResult Validate(string? name, ElasticSearchOptions optionses)
    {
        if(string.IsNullOrWhiteSpace(optionses.Uri))
        {
            return ValidateOptionsResult.Fail("Env variable ELS__Uri is not defined.");
        }
        if(string.IsNullOrWhiteSpace(optionses.SerbianLawsIndex))
        {
            return ValidateOptionsResult.Fail("Env variable ELS__SerbianLawIndex is not defined.");
        }

        return ValidateOptionsResult.Success;
    }
}