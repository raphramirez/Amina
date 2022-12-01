using Amina.Infrastructure;
using Amina.Infrastructure.Persistence;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Primitives;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddIdentityServerInfrastructure(builder.Configuration);

    var app = builder.Build();

    await app.Services.InitializeDatabasesAsync();

    app.UseIdentityServerInfrastructure(builder.Configuration, builder.Environment);

    app.MapIdentityServerEndpoints();

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException")
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}