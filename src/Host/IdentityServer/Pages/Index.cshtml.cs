using Amina.IdentityServer.Multitenancy;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amina.IdentityServer.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public MultiTenantInfo TenantInfo { get; private set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        TenantInfo = HttpContext.GetMultiTenantContext<MultiTenantInfo>()?.TenantInfo;
    }
}