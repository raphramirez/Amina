using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Amina.IdentityServer.Multitenancy;

public static class DependencyInjection
{
    public static IServiceCollection AddMultitenancy(this IServiceCollection services)
    {
        services.AddMultiTenant<MultiTenantInfo>()
            .WithBasePathStrategy(options => options.RebaseAspNetCorePathBase = true)
            .WithEFCoreStore<TenantDbContext, MultiTenantInfo>()
            .WithPerTenantAuthentication();

        return services;
    }
}