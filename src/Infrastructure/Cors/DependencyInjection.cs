using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Amina.Infrastructure.Cors;

internal static class DependencyInjection
{
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy(
                name: "AminaAllowSpecifcOrigins",
                policy =>
                {
                    policy.WithOrigins("http://identityserver.fbc2f3a5fa0f43d7adce.eastasia.aksapp.io")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });
    }

    internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
        app.UseCors("AminaAllowSpecifcOrigins");
}