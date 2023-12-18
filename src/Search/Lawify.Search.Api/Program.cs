using dotenv.net;
using Serilog.Sinks.Http.Private;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Fluent()
    .WithoutExceptions()
    .WithTrimValues()
    .WithEncoding(Encoding.UTF8WithoutBom)
    .WithoutOverwriteExistingVars()
    .Load();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
