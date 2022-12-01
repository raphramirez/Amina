using Amina.Infrastructure.Multitenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Amina.Infrastructure.Persistence.Initialization;

internal class TenantDbInitializer
{
    private readonly TenantDbContext _tenantDbContext;
    private readonly IConfiguration _config;

    public TenantDbInitializer(TenantDbContext tenantDbContext, IConfiguration config)
    {
        _tenantDbContext = tenantDbContext;
        _config = config;
    }

    public async Task InitializeTenantAdminDbAsync(CancellationToken cancellationToken)
    {
        if (_tenantDbContext.Database.GetPendingMigrations().Any())
        {
            Log.Information("Applying Root Migrations.");
            await _tenantDbContext.Database.MigrateAsync(cancellationToken);
        }

        await SeedTenantsAsync(cancellationToken);
    }

    private async Task SeedTenantsAsync(CancellationToken cancellationToken)
    {
        if (await _tenantDbContext.TenantInfo.FindAsync(new object?[] { "root" }, cancellationToken: cancellationToken) is not null)
        {
            Log.Information("Tenants are already initialized.");
            return;
        }

        var rootTenant = new MultiTenantInfo
        {
            Id = "root",
            Name = "Root",
            Identifier = "root",
            ConnectionString = _config.GetConnectionString("DefaultConnection"),
            ApplicationConnectionString = _config.GetConnectionString("DefaultApplicationConnection"),
        };
        _tenantDbContext.TenantInfo.Add(rootTenant);

        var sampleTenant1 = new MultiTenantInfo
        {
            Id = "s2dioapps",
            Name = "Studio Apps",
            Identifier = "s2dioapps",
            ConnectionString = _config.GetConnectionString("Tenant_StudioApps"),
            ApplicationConnectionString = _config.GetConnectionString("Tenant_StudioApps_ApplicationConnection"),
        };
        _tenantDbContext.TenantInfo.Add(sampleTenant1);

        await _tenantDbContext.SaveChangesAsync(cancellationToken);
    }
}