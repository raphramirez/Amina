using Amina.Infrastructure.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amina.Infrastructure.Authorization;

internal static class DependencyInjection
{
    internal static IServiceCollection AddAuthorizations(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "amina_api");
            });
        });

        return services;
    }
}