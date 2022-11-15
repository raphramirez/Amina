using Amina.Infrastructure.Identity;
using Amina.Infrastructure.Multitenancy;
using Amina.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Amina.Infrastructure.Persistence.Initialization.Identity;

internal class IdentityDbSeeder
{
    private readonly MultiTenantInfo _currentTenant;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;

    public IdentityDbSeeder(
        MultiTenantInfo currentTenant,
        UserManager<ApplicationUser> userManager,
        CustomSeederRunner seederRunner)
    {
        _currentTenant = currentTenant;
        _userManager = userManager;
        _seederRunner = seederRunner;
    }

    public async Task SeedDatabaseAsync(IdentityDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedDefaultUsers();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedDefaultUsers()
    {
        var alice = new ApplicationUser
        {
            UserName = "AliceSmith@email.com",
            Email = "AliceSmith@email.com",
            EmailConfirmed = true,
        };

        Log.Information("Seeding Default User for '{tenantId}' Tenant.", _currentTenant.Id);

        await _userManager.CreateAsync(alice, "Pass123$");
    }
}