using Amina.Infrastructure.Authentication;
using Amina.Infrastructure.Authorization;
using Amina.Infrastructure.Multitenancy;
using Amina.Infrastructure.OpenApi;
using Amina.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amina.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddAuthentications()
            .AddAuthorizations()
            .AddWebApiMultitenancy()
            .AddPersistence(config);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
    {
        return builder
            .UseHttpsRedirection()
            .UseAuthentication()
            .UseMultiTenancy()
            .UseAuthorization()
            .UseOpenApiDocumentation();
        ;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers().RequireAuthorization("ApiScope");
        return builder;
    }
}