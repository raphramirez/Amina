using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Amina.Infrastructure.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("amina_api", "Amina API"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("super-secret-key".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris =
                {
                    "https://localhost:5001/signin-oidc",
                    "https://localhost:5002/signin-oidc",
                    "http://amina-identity-server:80/signin-oidc",
                    "http://identityserver.fbc2f3a5fa0f43d7adce.eastasia.aksapp.io/signin-oidc",
                },

                // where to redirect to after logout
                PostLogoutRedirectUris =
                {
                    "https://localhost:5001/signout-callback-oidc",
                    "https://localhost:5002/signout-callback-oidc",
                    "http://amina-identity-server:80/signout-callback-oidc",
                    "http://identityserver.fbc2f3a5fa0f43d7adce.eastasia.aksapp.io/signout-callback-oidc",
                },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "amina_api",
                },

                AllowedCorsOrigins = {
                    "http://identityserver.fbc2f3a5fa0f43d7adce.eastasia.aksapp.io",
                },
            },
        };
}