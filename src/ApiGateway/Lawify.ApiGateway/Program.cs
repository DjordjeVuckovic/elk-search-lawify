var builder = WebApplication.CreateBuilder(args);

const string corsPolicyName = "corsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseCors(corsPolicyName);

app.MapReverseProxy();

app.Run();