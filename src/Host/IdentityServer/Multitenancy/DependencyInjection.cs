using Microsoft.AspNetCore.Authentication.Cookies;

namespace Amina.IdentityServer.Multitenancy;

public static class DependencyInjection
{
    public static IServiceCollection AddMultitenancy(this IServiceCollection services)
    {
        services.AddMultiTenant<MultiTenantInfo>()
            .WithHostStrategy()
            .WithEFCoreStore<TenantDbContext, MultiTenantInfo>()
            .WithPerTenantAuthentication()
            .WithPerTenantOptions<CookieAuthenticationOptions>((o, tenant) =>
            {
                o.Cookie.Name = $".Identity_{tenant.Identifier}";
                o.LoginPath = "/Identity/Account/Login";
            });

        return services;
    }
}