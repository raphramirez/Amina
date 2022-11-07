namespace Amina.IdentityServer;

public static class DependencyInjection
{
    public static IServiceCollection AddAspNetServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddControllers();
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
}