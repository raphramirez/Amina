using Amina.WebAPI.Common.Errors;
using Amina.WebAPI.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Amina.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, AminaProblemDetailsFactory>();

        services.AddMappings();

        return services;
    }
}