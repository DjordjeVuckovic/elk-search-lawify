using Microsoft.Extensions.Options;

namespace Lawify.Common.Options;

public class ElkOptions : IValidateOptions<ElkOptions>
{
    private static readonly string[] ValidEnvironments = [
        "development",
        "staging",
        "production"
    ];

    public const string Elk = "ELK";
    public string HttpSinkRequestUri { get; init; } = string.Empty;

    public string ServiceName { get; init; } = string.Empty;

    public string Environment { get; init; } = string.Empty;

    public ValidateOptionsResult Validate(string? name, ElkOptions options)
    {
        return string.IsNullOrWhiteSpace(options.Environment) || !ValidEnvironments.Contains(options.Environment)
            ? ValidateOptionsResult.Success
            : string.IsNullOrWhiteSpace(options.HttpSinkRequestUri)
                ? ValidateOptionsResult.Fail("Env variable ELK__HttpSinkRequestUri is not defined.")
                : string.IsNullOrWhiteSpace(options.ServiceName)
                    ? ValidateOptionsResult.Fail("Env variable ELK__ServiceName is not defined.")
                    : string.IsNullOrWhiteSpace(options.Environment)
                        ? ValidateOptionsResult.Fail("Env variable ELK__Environment is not defined.")
                        : ValidateOptionsResult.Success;
    }
}