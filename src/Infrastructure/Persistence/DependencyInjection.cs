using Amina.Infrastructure.Multitenancy;
using Amina.Infrastructure.Persistence.Context;
using Amina.Infrastructure.Persistence.Initialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amina.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var tenantAdminConnectionString = config.GetConnectionString("TenantAdmin");

        if (string.IsNullOrEmpty(tenantAdminConnectionString))
        {
            throw new InvalidOperationException("Tenant Admin DB ConnectionString is not configured.");
        }

        services.AddDbContext<TenantDbContext>(m =>
            m.UseDatabase(tenantAdminConnectionString));

        services.AddDbContext<IdentityDbContext>();

        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<IdentityDbInitializer>()
            .AddTransient<IdentityDbSeeder>()
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

    internal static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string connectionString)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return builder.UseNpgsql(connectionString, e =>
             e.MigrationsAssembly("Migrators.PostgreSQL"));
    }
}