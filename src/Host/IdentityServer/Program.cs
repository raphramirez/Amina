using Amina.Infrastructure;
using Amina.Infrastructure.Persistence;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
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

    var forwardOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
        RequireHeaderSymmetry = false,
    };

    forwardOptions.KnownNetworks.Clear();
    forwardOptions.KnownProxies.Clear();

    // Microsoft.AspNetCore.Antiforgery.DefaultAntiforgery
    string connectionString = "DefaultEndpointsProtocol=https;AccountName=aminastorage;AccountKey=UW7DHNjKYq95qWOfIJ+dHSVzMZOV6afeB0Pjxw5Q/zFi6a16tSlnijL9Kh/S9f2ky5ZjNORAKVF7+AStFY1Rgw==;EndpointSuffix=core.windows.net";
    string containerName = "my-key-container";
    string blobName = "keys.xml";
    BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
    BlobClient blobClient = container.GetBlobClient(blobName);

    builder.Services.AddDataProtection()
        .PersistKeysToAzureBlobStorage(blobClient)
        .ProtectKeysWithAzureKeyVault(new Uri("https://amina-keyvault.vault.azure.net/keys/dataprotection"), new DefaultAzureCredential());

    // optional - provision the container automatically
    await container.CreateIfNotExistsAsync();

    builder.Services.AddIdentityServerInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.UseForwardedHeaders(forwardOptions);

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