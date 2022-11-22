using Amina.Infrastructure;
using Amina.Infrastructure.Persistence;
using Amina.WebAPI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Services
    .AddPresentation()
    .AddWebApiInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler("/error");

await app.Services.InitializeDatabasesAsync();

app.UseWebApiInfrastructure(builder.Configuration);

app.MapWebApiEndpoints();

app.Run();