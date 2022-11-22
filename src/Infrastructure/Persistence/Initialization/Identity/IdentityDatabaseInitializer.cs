using Amina.Infrastructure.Multitenancy;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Amina.Infrastructure.Persistence.Initialization.Identity;

internal class IdentityDatabaseInitializer : IDatabaseInitializer
{
    private readonly TenantDbContext _tenantDbContext;
    private readonly IConfiguration _config;
    private readonly TenantDbInitializer _tenantDbInitializer;
    private readonly IServiceProvider _serviceProvider;

    public IdentityDatabaseInitializer(TenantDbContext tenantDbContext, IServiceProvider serviceProvider, IConfiguration config, TenantDbInitializer tenantDbInitializer)
    {
        _tenantDbContext = tenantDbContext;
        _serviceProvider = serviceProvider;
        _config = config;
        _tenantDbInitializer = tenantDbInitializer;
    }

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {
        await _tenantDbInitializer.InitializeTenantAdminDbAsync(cancellationToken);

        foreach (var tenant in await _tenantDbContext.TenantInfo.ToListAsync(cancellationToken))
        {
            await InitializeDbForTenantAsync(tenant, cancellationToken);
        }
    }

    public async Task InitializeDbForTenantAsync(MultiTenantInfo tenant, CancellationToken cancellationToken)
    {
        // First create a new scope
        using var scope = _serviceProvider.CreateScope();

        // Then set current tenant so the right connectionstring is used
        _serviceProvider.GetRequiredService<IMultiTenantContextAccessor>()
            .MultiTenantContext = new MultiTenantContext<MultiTenantInfo>()
            {
                TenantInfo = tenant,
            };

        // Then run the initialization in the new scope
        await scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>()
            .InitializeAsync(cancellationToken);
    }
}