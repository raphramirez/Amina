using Amina.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Amina.Infrastructure.Persistence.Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();

        return services;
    }
}