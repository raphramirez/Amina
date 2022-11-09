using Amina.IdentityServer.Identity;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amina.IdentityServer.Areas.Identity.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LogoutModel> _logger;
    private IIdentityServerInteractionService _interaction;

    public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IIdentityServerInteractionService interaction)
    {
        _signInManager = signInManager;
        _logger = logger;
        _interaction = interaction;
    }

    public async Task<IActionResult> OnGet(string returnUrl = null)
    {
        return await OnPost(returnUrl);
    }

    public async Task<IActionResult> OnPost(string returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");

        var logoutId = Request.Query["logoutId"].ToString();

        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else if (!string.IsNullOrEmpty(logoutId))
        {
            var logoutContext = await _interaction.GetLogoutContextAsync(logoutId);
            returnUrl = logoutContext.PostLogoutRedirectUri;

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
        else
        {
            return Page();
        }
    }
}