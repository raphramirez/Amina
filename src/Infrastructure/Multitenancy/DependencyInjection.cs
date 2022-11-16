using Amina.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace Amina.Infrastructure.Multitenancy;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityServerMultitenancy(this IServiceCollection services)
    {
        services
            .AddMultiTenant<MultiTenantInfo>()
            .WithBasePathStrategy(options => options.RebaseAspNetCorePathBase = true)
            .WithEFCoreStore<TenantDbContext, MultiTenantInfo>()
            .WithPerTenantAuthentication()
            .WithPerTenantOptions<CookieAuthenticationOptions>((o, tenant) =>
            {
                o.Cookie.Name = $".Identity_{tenant.Identifier}";
                o.LoginPath = "/Identity/Account/Login";
            });

        return services;
    }

    public static IServiceCollection AddWebApiMultitenancy(this IServiceCollection services)
    {
        services.AddMultiTenant<MultiTenantInfo>()
           .WithHeaderStrategy("tenant")
           .WithQueryStringStrategy("tenant")
           .WithEFCoreStore<TenantDbContext, MultiTenantInfo>()
           .WithRemoteAuthenticationCallbackStrategy()
           .WithPerTenantOptions<JwtBearerOptions>((options, tenant) =>
           {
               options.Authority = $"https://localhost:5001/{tenant.Identifier}/";

               options.TokenValidationParameters.ValidateAudience = false;
           });

        return services;
    }

    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app) =>
        app.UseMultiTenant();

    private static FinbuckleMultiTenantBuilder<MultiTenantInfo> WithQueryStringStrategy(this FinbuckleMultiTenantBuilder<MultiTenantInfo> builder, string queryStringKey) =>
        builder.WithDelegateStrategy(context =>
        {
            if (context is not HttpContext httpContext)
            {
                return Task.FromResult((string?)null);
            }

            httpContext.Request.Query.TryGetValue(queryStringKey, out StringValues tenantIdParam);

            return Task.FromResult((string?)tenantIdParam.ToString());
        });
}