using Amina.Infrastructure.Multitenancy;
using Amina.Infrastructure.Persistence.Context;
using Serilog;

namespace Amina.Infrastructure.Persistence.Initialization.Application;

internal class ApplicationDbSeeder
{
    private readonly MultiTenantInfo _currentTenant;
    private readonly CustomSeederRunner _seederRunner;

    public ApplicationDbSeeder(
        MultiTenantInfo currentTenant,
        CustomSeederRunner seederRunner)
    {
        _currentTenant = currentTenant;
        _seederRunner = seederRunner;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedDefaultData();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedDefaultData()
    {
        Log.Information("Seeding Default Data for '{tenantId}' Tenant.", _currentTenant.Id);
    }
}