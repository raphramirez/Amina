using Amina.IdentityServer.Multitenancy;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Amina.IdentityServer.Pages
{
    [AllowAnonymous]
    public class Index : PageModel
    {
        public string Version;

        public string TenantId;
        public string TenantName;

        public Index(IMultiTenantContextAccessor<MultiTenantInfo> Tenant)
        {
            TenantId = Tenant.MultiTenantContext?.TenantInfo?.Identifier ?? "no-tenant";
            TenantName = Tenant.MultiTenantContext?.TenantInfo?.Name ?? "no-tenant";
        }

        public void OnGet()
        {
            Version = typeof(Duende.IdentityServer.Hosting.IdentityServerMiddleware).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+').First();
        }
    }
}