using Microsoft.Extensions.Options;

namespace Lawify.Content.Api.Configuration;

public class MinioOptions : IValidateOptions<MinioOptions>
{
    public const string Minio = nameof(Minio);
    public string? Endpoint {get; init;}
    public string? AccessKey {get; init;}
    public string? SecretKey {get; init;}
    public string? LawsBucketName {get; init;}
    public string? ContractsBucketName {get; init;}

    public ValidateOptionsResult Validate(string? name, MinioOptions options)
    {
        if(string.IsNullOrWhiteSpace(options.Endpoint))
        {
            return ValidateOptionsResult.Fail($"Env variable {Minio}__{nameof(Endpoint)} is not defined");
        }
        return ValidateOptionsResult.Success;
    }
}