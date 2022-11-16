using Amina.Infrastructure.Persistence.Context;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Amina.Infrastructure.Persistence.Initialization.Application;

internal class ApplicationDbInitializer
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ITenantInfo _currentTenant;
    private readonly ApplicationDbSeeder _dbSeeder;

    public ApplicationDbInitializer(ApplicationDbContext applicationDbContext, ITenantInfo currentTenant, ApplicationDbSeeder dbSeeder)
    {
        _applicationDbContext = applicationDbContext;
        _currentTenant = currentTenant;
        _dbSeeder = dbSeeder;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_applicationDbContext.Database.GetMigrations().Any())
        {
            if ((await _applicationDbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                Log.Information("Applying Application Migrations for '{tenantId}' tenant.", _currentTenant.Id);
                await _applicationDbContext.Database.MigrateAsync(cancellationToken);
            }

            if (await _applicationDbContext.Database.CanConnectAsync(cancellationToken))
            {
                Log.Information("Connection to {tenantId}'s Application Database Succeeded.", _currentTenant.Id);

                await _dbSeeder.SeedDatabaseAsync(_applicationDbContext, cancellationToken);
            }
        }
    }
}