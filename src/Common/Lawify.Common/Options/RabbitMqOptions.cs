using Microsoft.Extensions.Options;

namespace Lawify.Common.Options;

public class RabbitMqOptions : IValidateOptions<RabbitMqOptions>
{
    public const string RabbitMq = nameof(RabbitMq);
    public string? Host { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public ValidateOptionsResult Validate(string? name, RabbitMqOptions options)
    {
        if (options.Host is null || options.Username is null || options.Password is null) {
            return ValidateOptionsResult.Fail("RabbitMq options are not defined.");
        }
        return ValidateOptionsResult.Success;
    }
}