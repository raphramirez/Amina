using Amina.Infrastructure.Multitenancy;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amina.IdentityServer.Pages;

public class IndexModel : PageModel
{
    public IndexModel()
    {
    }

    public MultiTenantInfo? TenantInfo { get; private set; }

    public void OnGet()
    {
        TenantInfo = HttpContext.GetMultiTenantContext<MultiTenantInfo>()?.TenantInfo;
    }
}