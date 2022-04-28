using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace IdentityServer;

public static class Config
{
  public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[]
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
      };

  public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[]
          {
              new ApiScope(name: "flooqapi", displayName: "FlooqApi")
           };

  public static IEnumerable<Client> Clients =>
      new Client[]
          {
            new Client {
              ClientId = "editor",
              // TODO: This is only for the editor application to access the API!
              AllowedGrantTypes = GrantTypes.ClientCredentials,
              ClientSecrets =
              {
                  new Secret("secret".Sha256())
                  // TODO: Use generated secred here
              },
              AllowedScopes = { "flooqapi" },
              AllowedCorsOrigins = { "http://localhost:8080"}
            },
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect after login
                RedirectUris = { "https://localhost:5002/signin-oidc", "http://localhost:8080/swagger/oauth2-redirect.html", "http://localhost:3000/api/auth/callback/flooq" },

                // where to redirect after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                AllowedCorsOrigins = { "https://localhost:5002" },
                RequirePkce = false,
                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "flooqapi"
                }
            }
          };
}