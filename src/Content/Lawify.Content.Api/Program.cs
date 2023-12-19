using System.Text;
using Carter;
using dotenv.net;
using Lawify.Common.Middlewares;
using Lawify.Content.Api.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

DotEnv.Fluent()
    .WithoutExceptions()
    .WithTrimValues()
    .WithEncoding(Encoding.UTF8)
    .WithoutOverwriteExistingVars()
    .Load();

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

builder.Services.AddHealthChecks();

builder.Services.AddCarter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseRouting();

app.MapHealthChecks("/health/ready", new HealthCheckOptions {
    Predicate = check => check.Tags.Contains("ready")
});

app.MapHealthChecks("/health/live", new HealthCheckOptions());

app.MapCarter();
app.UseHttpsRedirection();

app.Run();