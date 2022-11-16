using Amina.Infrastructure;
using Amina.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Services.AddControllers();
builder.Services.AddWebApiInfrastructure(builder.Configuration);

var app = builder.Build();

await app.Services.InitializeDatabasesAsync();

app.UseWebApiInfrastructure(builder.Configuration);

app.MapWebApiEndpoints();

app.Run();