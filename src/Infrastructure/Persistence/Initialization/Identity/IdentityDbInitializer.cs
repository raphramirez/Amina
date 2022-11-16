using Amina.Infrastructure.Persistence.Context;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Amina.Infrastructure.Persistence.Initialization.Identity;

internal class IdentityDbInitializer
{
    private readonly IdentityDbContext _identityDbContext;
    private readonly ITenantInfo _currentTenant;
    private readonly IdentityDbSeeder _dbSeeder;

    public IdentityDbInitializer(IdentityDbContext identityDbContext, ITenantInfo currentTenant, IdentityDbSeeder dbSeeder)
    {
        _identityDbContext = identityDbContext;
        _currentTenant = currentTenant;
        _dbSeeder = dbSeeder;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_identityDbContext.Database.GetMigrations().Any())
        {
            if ((await _identityDbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                Log.Information("Applying Identity Migrations for '{tenantId}' tenant.", _currentTenant.Id);
                await _identityDbContext.Database.MigrateAsync(cancellationToken);
            }

            if (await _identityDbContext.Database.CanConnectAsync(cancellationToken))
            {
                Log.Information("Connection to {tenantId}'s Identity Database Succeeded.", _currentTenant.Id);

                await _dbSeeder.SeedDatabaseAsync(_identityDbContext, cancellationToken);
            }
        }
    }
}