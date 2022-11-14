using Serilog;
using Amina.IdentityServer.IdentityServer;
using Amina.Infrastructure.Persistence;
using Amina.Infrastructure.Identity;
using Amina.Infrastructure.Multitenancy;

namespace Amina.IdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddPersistence(builder.Configuration)
                .AddIdentity()
                .AddDuendeIdentityServer()
                .AddExternalAuthentication()
                .AddIdentityServerMultitenancy()
                .AddAspNetServices();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMultiTenancy();

            app.UseRouting();

            //app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllers();

            return app;
        }
    }
}