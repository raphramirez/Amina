using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Amina.Infrastructure.Authentication;

internal static class DependencyInjection
{
    internal static IServiceCollection AddAuthentications(this IServiceCollection services)
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        return services;
    }
}