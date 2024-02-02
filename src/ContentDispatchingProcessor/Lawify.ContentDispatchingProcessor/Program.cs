using System.Text;
using dotenv.net;
using Lawify.Common.Middlewares;
using Lawify.ContentDispatchingProcessor.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

DotEnv.Fluent()
    .WithoutExceptions()
    .WithTrimValues()
    .WithEncoding(Encoding.UTF8)
    .WithoutOverwriteExistingVars()
    .Load();

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapHealthChecks("/health/ready", new HealthCheckOptions {
    Predicate = check => check.Tags.Contains("ready")
});

app.MapHealthChecks("/health/live", new HealthCheckOptions());

app.Run();