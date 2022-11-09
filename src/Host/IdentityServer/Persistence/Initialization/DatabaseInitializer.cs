using Amina.IdentityServer.Multitenancy;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Amina.IdentityServer.Persistence.Initialization;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly TenantDbContext _tenantDbContext;
    private readonly IConfiguration _config;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(TenantDbContext tenantDbContext, IServiceProvider serviceProvider, IConfiguration config)
    {
        _tenantDbContext = tenantDbContext;
        _serviceProvider = serviceProvider;
        _config = config;
    }

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {
        await InitializeTenantDbAsync(cancellationToken);

        foreach (var tenant in await _tenantDbContext.TenantInfo.ToListAsync(cancellationToken))
        {
            await InitializeApplicationDbForTenantAsync(tenant, cancellationToken);
        }
    }

    public async Task InitializeApplicationDbForTenantAsync(MultiTenantInfo tenant, CancellationToken cancellationToken)
    {
        // First create a new scope
        using var scope = _serviceProvider.CreateScope();

        // Then set current tenant so the right connectionstring is used
        _serviceProvider.GetRequiredService<IMultiTenantContextAccessor>()
            .MultiTenantContext = new MultiTenantContext<MultiTenantInfo>()
            {
                TenantInfo = tenant
            };

        // Then run the initialization in the new scope
        await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
            .InitializeAsync(cancellationToken);
    }

    private async Task InitializeTenantDbAsync(CancellationToken cancellationToken)
    {
        if (_tenantDbContext.Database.GetPendingMigrations().Any())
        {
            Log.Information("Applying Root Migrations.");
            await _tenantDbContext.Database.MigrateAsync(cancellationToken);
        }

        await SeedRootTenantAsync(cancellationToken);
    }

    private async Task SeedRootTenantAsync(CancellationToken cancellationToken)
    {
        if (await _tenantDbContext.TenantInfo.FindAsync(new object?[] { "root" }, cancellationToken: cancellationToken) is not null)
        {
            Log.Error("SeedRootTenantAsync returns");
            return;
        }

        var rootTenant = new MultiTenantInfo
        {
            Id = "root",
            Name = "Root",
            Identifier = "root",
            ConnectionString = _config.GetConnectionString("DefaultConnection")
        };
        _tenantDbContext.TenantInfo.Add(rootTenant);

        var sampleTenant1 = new MultiTenantInfo
        {
            Id = "s2dioapps",
            Name = "Studio Apps",
            Identifier = "s2dioapps",
            ConnectionString = _config.GetConnectionString("Tenant_StudioApps")
        };
        _tenantDbContext.TenantInfo.Add(sampleTenant1);

        await _tenantDbContext.SaveChangesAsync(cancellationToken);
    }
}