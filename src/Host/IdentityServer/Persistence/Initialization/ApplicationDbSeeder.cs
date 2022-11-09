using Amina.IdentityServer.Identity;
using Amina.IdentityServer.Multitenancy;
using Amina.IdentityServer.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Amina.IdentityServer.Persistence.Initialization;

internal class ApplicationDbSeeder
{
    private readonly MultiTenantInfo _currentTenant;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(
        MultiTenantInfo currentTenant,
        UserManager<ApplicationUser> userManager,
        CustomSeederRunner seederRunner,
        ILogger<ApplicationDbSeeder> logger)
    {
        _currentTenant = currentTenant;
        _userManager = userManager;
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
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

        _logger.LogInformation("Seeding Default User for '{tenantId}' Tenant.", _currentTenant.Id);

        await _userManager.CreateAsync(alice, "Pass123$");
    }
}