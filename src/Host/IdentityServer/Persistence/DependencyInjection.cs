using Amina.IdentityServer.Multitenancy;
using Amina.IdentityServer.Persistence.Context;
using Amina.IdentityServer.Persistence.Initialization;
using Microsoft.EntityFrameworkCore;

namespace Amina.IdentityServer.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var tenantAdminConnectionString = config.GetConnectionString("TenantAdmin");

        if (string.IsNullOrEmpty(tenantAdminConnectionString))
        {
            throw new InvalidOperationException("DB ConnectionString is not configured.");
        }

        services.AddDbContext<TenantDbContext>(options =>
            options.UseNpgsql(tenantAdminConnectionString));

        services.AddDbContext<ApplicationDbContext>();

        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<ApplicationDbInitializer>()
            .AddTransient<ApplicationDbSeeder>()
            .AddTransient<CustomSeederRunner>();

        return services;
    }

    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabasesAsync(cancellationToken);
    }
}