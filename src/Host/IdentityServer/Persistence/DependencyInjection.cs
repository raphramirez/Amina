using Amina.IdentityServer.Multitenancy;
using Amina.IdentityServer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Amina.IdentityServer.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TenantDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("TenantAdmin")));

        services.AddDbContext<ApplicationDbContext>();

        return services;
    }
}