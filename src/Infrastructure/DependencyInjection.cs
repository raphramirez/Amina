using Amina.Infrastructure.Authentication;
using Amina.Infrastructure.Authorization;
using Amina.Infrastructure.Identity;
using Amina.Infrastructure.IdentityServer;
using Amina.Infrastructure.Multitenancy;
using Amina.Infrastructure.OpenApi;
using Amina.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Amina.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddPersistence(config)
            .AddApplicationDatabase()
            .AddAuthentications()
            .AddAuthorizations()
            .AddWebApiMultitenancy()
            .AddOpenApiDocumentation();
    }

    public static IApplicationBuilder UseWebApiInfrastructure(this IApplicationBuilder builder, IConfiguration config)
    {
        return builder
            .UseHttpsRedirection()
            .UseAuthentication()
            .UseMultiTenancy()
            .UseAuthorization()
            .UseOpenApiDocumentation();
        ;
    }

    public static IServiceCollection AddIdentityServerInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
                .AddPersistence(config)
                .AddIdentityDatabase()
                .AddIdentity()
                .AddDuendeIdentityServer()
                .AddExternalAuthentication()
                .AddIdentityServerMultitenancy()
                .AddAspNetServices();
    }

    public static IApplicationBuilder UseIdentityServerInfrastructure(this IApplicationBuilder builder, IConfiguration config, IWebHostEnvironment env)
    {
        builder.UseSerilogRequestLogging();

        if (env.IsDevelopment())
        {
            builder.UseDeveloperExceptionPage();
        }

        return builder
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseMultiTenancy()
            .UseRouting()
            .UseIdentityServer()
            .UseAuthorization();
    }

    public static IEndpointRouteBuilder MapWebApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization("ApiScope");
        return builder;
    }

    public static IEndpointRouteBuilder MapIdentityServerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapRazorPages();
        builder.MapControllers();
        return builder;
    }

    private static IServiceCollection AddAspNetServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddControllers();
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
}